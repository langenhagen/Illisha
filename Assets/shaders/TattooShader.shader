Shader "Barn/Character"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}

		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        
        _eyeCenters ("Eye Centers (x1,y1, x2,y2)", Vector) = (0, 0, 0, 0)
        _pupil1Size ("Pupil 1 size", Float) = 0.1
        _pupil2Size ("Pupil 2 size", Float) = 0.1
        
        
        _sprite1 ("Sprite 1", 2D) = "white" {}
        
        [MaterialToggle] _dontUseTextureAlphaSprite1 ("Sprite 1 dont use Texture alpha", Float) = 0
        
        _sprite1OffX    ("Sprite 1 Offset X", Float) = 0
        _sprite1OffY    ("Sprite 1 Offset Y", Float) = 0
        _sprite1ScaleX  ("Sprite 1 Scale X",  Float) = 1
        _sprite1ScaleY  ("Sprite 1 Scale Y",  Float) = 1
        _sprite1Angle   ("Sprite 1 Angle",    Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

            
            
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
            };
            
            
            sampler2D _MainTex;
            sampler2D _sprite1;

            float4 _eyeCenters;
            float _pupil1Size;
            float _pupil2Size;
            
            bool _dontUseTextureAlphaSprite1;
            

            float _sprite1OffX;
            float _sprite1OffY;
            
            float _sprite1ScaleX;
            float _sprite1ScaleY;
            
            float _sprite1Angle;
                 
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
				
                #ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

                
				return OUT;
			}
            			
            
			fixed4 frag(v2f IN) : COLOR
			{
                float4 ret = tex2D(_MainTex, IN.texcoord);

                
                ///// pupils /////
                
                float2 eye1Cathetes = float2( IN.texcoord.x - _eyeCenters.x, IN.texcoord.y - _eyeCenters.y);
                float2 eye2Cathetes = float2( IN.texcoord.x - _eyeCenters.z, IN.texcoord.y - _eyeCenters.w);
                                
                
                float squaredPixelPosOnEye1 = dot( eye1Cathetes, eye1Cathetes);
                float squaredPixelPosOnEye2 = dot( eye2Cathetes, eye2Cathetes);;
                float squaredPupil1Size = dot(_pupil1Size,_pupil1Size);
                float squaredPupil2Size = dot(_pupil2Size,_pupil2Size);
                
                
                if( squaredPixelPosOnEye1 <= squaredPupil1Size)
                {
                    float eye1Alpha = 1-clamp( 10000*( squaredPupil1Size - squaredPixelPosOnEye1), 0, 1);
                    ret.rgb *= float3(eye1Alpha,eye1Alpha,eye1Alpha);
                }
                
                if(squaredPixelPosOnEye2 <= squaredPupil2Size)
                {
                    float eye2Alpha = 1-clamp( 10000*( squaredPupil2Size - squaredPixelPosOnEye2), 0, 1);
                    ret.rgb *= float3(eye2Alpha,eye2Alpha,eye2Alpha);
                }
                
                ///// sprites /////
                
                float2 offset = float2(0.5,0.5);
                float c, s;
                float2x2 rotation;
                float2 spriteCoord;
                float4 spriteColor;
                
                // rotate, scale, move tha 1st sprite
                
                sincos(radians(_sprite1Angle) , s, c);
                rotation = float2x2(c, -s, s ,c);
            
                spriteCoord = mul( rotation, IN.texcoord - float2( _sprite1OffX, _sprite1OffY) ) / float2( _sprite1ScaleX, _sprite1ScaleY) + offset;
                spriteColor = tex2D(_sprite1, spriteCoord);
                
                ret.rgb *= (1-spriteColor.a) + spriteColor.rgb * ( spriteColor.a);

                if( _dontUseTextureAlphaSprite1)
                {
                    ret.a += ( 1-ret.a ) * spriteColor.a;
                }
                return ret;
			}

            
		ENDCG
		}
	}
    
    
    
    

}

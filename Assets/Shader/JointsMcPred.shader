Shader "Custom/JointsMcPred"
{
	Properties {
		_Smoothness ("Smoothness", Range(0,1)) = 0.5
	}
	
    SubShader{ 
        CGPROGRAM
        #pragma surface ConfigureSurface Standard fullforwardshadows addshadow
        #pragma instancing_options assumeuniformscaling procedural:ConfigureProcedural
        #pragma target 4.5

        struct Input {
			float3 worldPos;
		};

        float4 _Origin;
        float4 _Uarm;
		float4 _Larm;
        float4 _Hand;
        
        float4 color;
        float _Smoothness;
        float _Scale;
        

        #if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
			StructuredBuffer<float3> _Positions;
        #endif

        void ConfigureProcedural ()
        {
	        #if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
				float3 position = _Positions[unity_InstanceID];        	
				unity_ObjectToWorld = 0.0;
        		unity_ObjectToWorld._m00_m11_m22 = _Scale;
				unity_ObjectToWorld._m03_m13_m23_m33 = float4(position, 1.0) + _Origin;
	        #endif
        }

        void ConfigureSurface (Input input, inout SurfaceOutputStandard surface)
		{
        	//TODO: optimize this further by separating into two buffers
            float du = 1.0 - distance(input.worldPos,_Uarm + _Origin) * 9.;
            float dl = 1.0 - distance(input.worldPos,_Larm + _Origin) * 9.;
            float dh = 1.0 - distance(input.worldPos,_Hand + _Origin) * 9.;
        	float d = max(dl, dh);
        	d = max(d, du);
			surface.Albedo = float4(1.0-d, d, 0, 1.0);
			surface.Smoothness = _Smoothness;
		}ENDCG
    }
    FallBack "Diffuse"
}

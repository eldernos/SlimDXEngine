cbuffer globals
{
	matrix finalMatrix;
}

struct VS_IN
{
	float3 pos : POSITION;
	float2 uv : TEXCOORD0;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
};

// Vertex Shader
PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = mul(float4(input.pos, 1), finalMatrix);
	output.uv = input.uv;
	
	return output;
}

Texture2D yodaTexture;
SamplerState currentSampler
{
	Filter = MIN_MAG_MIP_LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
};

// Pixel Shader
float4 PS( PS_IN input ) : SV_Target
{
	return yodaTexture.Sample(currentSampler, input.uv);
}

// Technique
technique10 Render
{
	pass P0
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VS() ) );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}
}


// Colored Vertexes


struct VS_IN_C
{
	float4 pos : POSITION;
	float4 col : COLOR;
};

struct PS_IN_C
{
	float4 pos : SV_POSITION;
	float4 col : COLOR;
};

PS_IN_C VSC( VS_IN_C input )
{
	PS_IN_C output = (PS_IN_C)0;
	
	output.pos = mul(input.pos, finalMatrix);
	output.col = input.col;
	
	return output;
}

float4 PSC( PS_IN_C input ) : SV_Target
{
	return input.col;
}

technique10 RenderC
{
	pass P0
	{
		SetVertexShader( CompileShader( vs_4_0, VSC() ) );
		SetPixelShader( CompileShader( ps_4_0, PSC() ) );
	}
	
}
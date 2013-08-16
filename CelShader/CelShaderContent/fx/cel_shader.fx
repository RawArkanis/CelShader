// Matrix
float4x4 World;
float4x4 View;
float4x4 Projection;

// Light
float3 LightDirection;
float4 LightColor;
float LightIntensity;
float4 AmbientColor;
float AmbientIntensity;

// Scale
float Scale;

// Camera
float3 CameraPosition;

// Texture
texture ColorMap;
texture CelMap;
texture EdgeMap;

sampler ColorMapSampler = sampler_state
{
	Texture = <ColorMap>;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
};

sampler CelMapSampler = sampler_state
{
	Texture = <CelMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = Clamp;
	AddressV = Clamp;
};

sampler EdgeMapSampler = sampler_state
{
	Texture = <EdgeMap>;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	MipFilter = LINEAR;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float2 T : TEXCOORD0;
	float3 N : NORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float2 T : TEXCOORD0;
	float3 L : TEXCOORD1;
	float3 N : TEXCOORD2;
	float Edge : TEXCOORD3;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

	output.T = input.T;

    output.L = normalize(LightDirection);
	output.N = normalize(mul(input.N, World));

	float3 eye = normalize(CameraPosition - worldPosition.xyz);
	output.Edge = max(dot(output.N, eye), 0.0f);

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 color = tex2D(ColorMapSampler, input.T);

    float2 celIndex = float2(saturate(-dot(input.L, input.N)), 0.0f);
	float4 celColor = tex2D(CelMapSampler, celIndex);

	float4 edgeColor = tex2D(EdgeMapSampler, input.Edge);

	return edgeColor * ((color * AmbientColor * AmbientIntensity) + (color * LightColor * celColor * LightIntensity));
}

technique CelShader
{
    pass Pass1
    {
		Sampler[0] = (ColorMapSampler);
		Sampler[1] = (CelMapSampler);
		Sampler[2] = (EdgeMapSampler);

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

float4x4 World;
float4x4 InverseWorld;
float4x4 View;
float4x4 Projection;

float3 LightDirection;

struct VertexShaderInput
{
    float4 Position : POSITION0;
	float3 N : NORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
	float3 L : TEXCOORD0;
	float3 N : TEXCOORD1;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.L = normalize(LightDirection);
	output.N = normalize(mul(InverseWorld, input.N));

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float4 color = float4(0.5f, 0.7f, 0.0f, 1.0f);

    float intensity = saturate(-dot(input.L, input.N));

	float celValue = 0.75f;
	if (intensity <= 0.25f)
		celValue = 0.25f;
	else if (intensity <= 0.5f)
		celValue = 0.5f;

    return celValue * color;
}

technique CelShader
{
    pass Pass1
    {
        // TODO: set renderstates here.

        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

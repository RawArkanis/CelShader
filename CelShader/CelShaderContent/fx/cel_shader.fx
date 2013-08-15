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
	float3 L : TEXCOORD1;
	float3 N : TEXCOORD2;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, World);
    float4 viewPosition = mul(worldPosition, View);
    output.Position = mul(viewPosition, Projection);

    output.L = normalize(LightDirection);
	output.N = normalize(mul(World, input.N));

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float value = max(dot(input.L, input.N), 0);

	float color = 0.8f;
	if (value < 0.05f)
		color = 0.3f;
	else if (value < 0.5f)
		color = 0.5f; 

    return color;
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

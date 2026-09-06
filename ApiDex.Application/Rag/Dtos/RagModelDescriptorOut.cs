namespace ApiDex.Application.Rag.Dtos;

public sealed record RagModelDescriptorOut(string Type, string Key, int Dimensions, bool Offline);

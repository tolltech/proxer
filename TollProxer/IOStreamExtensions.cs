public static class IOStreamExtensions
{
    public static byte[] ReadBytes(this Stream stream)
    {
        var bytes = new List<byte>();
        while (true)
        {
            var bt = stream.ReadByte();
            if (bt == -1)
                break;
            bytes.Add((byte)bt);
        }

        return bytes.ToArray();
    }
}
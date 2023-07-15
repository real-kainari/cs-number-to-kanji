using System.Collections.Generic;

class  Program
{
    static void Main(string[] args)
    {
        Console.Write("自然数を入力して下さい。請輸入自然數。请输入自然数。\ninput : ");

        var str = Console.ReadLine();

        try
        {
            uint num = uint.Parse(str);
            Console.Write($"日本語　 : {Convert.ToJapanese(num)}\n");
            Console.Write($"繁體中文 : {Convert.ToChinese(num)}\n");
            Console.Write($"简体中文 : {Convert.ToChinese(num, true)}\n");
        }
        catch (Exception)
        {
            Console.Write($"{str}は自然数ではありません。{str}不是自然數。{str}不是自然数。\n");
        }
    }
}
class Convert
{
    public enum Language
    {
        Japanese,       // 日本語
        Traditional,    // 繁體中文
        Simplified,     // 简体中文
    }

    public static string ToJapanese(uint num)
    {
        string[] digits = GetDigits(Language.Japanese);
        string[] powers = GetPowers(Language.Japanese);
        string[] units = GetUnits(Language.Japanese);

        if (num == 0)
            return digits[0];

        string kanji = "";
        int digitNumber = GetDigitNumber(num);
        int existsZero = -1;

        for (int i = digitNumber; i >= 0 ; i--)
        {
            int digit = GetDigit(num, i);
            int power = i % 4;
            int unit = power == 0 ? i / 4 : 0;
            string digitStr = digits[digit];
            string powerStr = powers[power];
            string unitStr = units[unit];

            if (digit == 0)
            {
                digitStr = "";
                powerStr = "";

                if (existsZero == -1)
                    existsZero = power;
            }

            if (digit == 1 && 0 < power  && ((power < 4 && i < 4) || (power < 3 && 4 <= i)))
                digitStr = "";

            if (existsZero == 3 && unit != 0)
                unitStr = "";

            if (digit != 0 || power == 0)
                existsZero = -1;

            kanji += digitStr + powerStr + unitStr;
        }

        return kanji;
    }

    public static string ToChinese(uint num, bool isSimplified = false)
    {
        Language language = isSimplified ? Language.Simplified : Language.Traditional;
        string two = GetTwo(language);
        string[] digits = GetDigits(language);
        string[] powers = GetPowers(language);
        string[] units = GetUnits(language);

        if (num == 0)
            return digits[0];

        string hanzi = "";
        int digitNumber = GetDigitNumber(num);
        int existsZero = -1;
        int firstZero = digitNumber % 4 == 3 ? -1 : digitNumber % 4 + 1;

        for (int i = digitNumber; i >= 0 ; i--)
        {
            int digit = GetDigit(num, i);
            int power = i % 4;
            int unit = power == 0 ? i / 4 : 0;
            string digitStr = digits[digit];
            string powerStr = powers[power];
            string unitStr = units[unit];

            if (digit == 0)
            {
                digitStr = "";
                powerStr = "";

                if (existsZero == -1)
                    existsZero = power;

                if (firstZero == -1)
                    firstZero = power;
            }

            if (digit == 2 && ((power == 0 && i == digitNumber) || power == 3))
                digitStr = two;

            if (existsZero > 0 && digit != 0)
                digitStr = digits[0] + digitStr;

            if ((existsZero == 0 || existsZero == 1) && (firstZero == 0 || firstZero == 1) && digit == 0 && power == 0)
                hanzi = hanzi.Remove(hanzi.Length - 1, 1);

            if ((digit == 0 && power == 0) || (digit == 1 && power == 1 && i == digitNumber))
                digitStr = "";

            if (existsZero == 3 && unit != 0)
                unitStr = "";

            if (digit != 0 || power == 0)
                existsZero = -1;

            if (power == 0)
                firstZero = -1;

            hanzi += digitStr + powerStr + unitStr;
        }

        return hanzi;
    }

    private static string GetTwo(Language language)
    {
        switch (language)
        {
            case Language.Traditional:
                return "兩";
            case Language.Simplified:
                return "两";
            default:
                return "";
        }
    }
    
    private static string[] GetDigits(Language language)
    {
        switch (language)
        {
            case Language.Japanese:
            case Language.Simplified:
                return new string[] { "〇", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            case Language.Traditional:
                return new string[] { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            default:
                return new string[] { };
        }
    }

    private static string[] GetPowers(Language language)
    {
        switch (language)
        {
            case Language.Japanese:
            case Language.Traditional:
            case Language.Simplified:
                return new string[] { "", "十", "百", "千" };
            default:
                return new string[] { };
        }
    }

    private static string[] GetUnits(Language language)
    {
        switch (language)
        {
            case Language.Japanese:
                return new string[] { "", "万", "億" };
            case Language.Traditional:
                return new string[] { "", "萬", "億" };
            case Language.Simplified:
                return new string[] { "", "万", "亿" };
            default:
                return new string[] { };
        }
    }


    public static int GetDigit(uint num, int n)
    {
        // numのn桁目の値を返す
        return (int)(num % Math.Pow(10, (double)n + 1) / Math.Pow(10, (double)n));
    }

    public static int GetDigitNumber(uint num)
    {
        // numの桁数を返す
        return num == 0 ? 1 : (int)Math.Log10(num);
    }
}
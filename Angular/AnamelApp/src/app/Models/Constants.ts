export const Constants = {   
    ArabicLettersRegx: `[\u0600-\u06FF\s]`,
    EnglishLettersRegx: `^[A-Za-z\s]+$`,
    NumbersRegx: `^[0-9]*$`,
    DateMDYRegx: `^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/(19|20)\d{2}$`,
    DateDMYRegx: `^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$`,
    EmailReg: `[a-zA-Z0-9._%+-]{1,}@[a-zA-Z0-9.-]{2,}[.]{1}[a-zA-Z]{2,}$`,
}

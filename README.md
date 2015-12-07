# Proquint.NET
Proquint generator for .NET.

A Proquint is a PRO-nouncable QUINT-uplet of alternating unambiguous consonants and vowels.

A 32-bit implementation is provided (so there are 2^32 different proquints), each of which is a string consisting on two quintuplet strings separated by the character '-'.

Some examples:

| index | Proquint |
| ------- | ----------- |
| 0 | *babab-babab* |
| 1 | *babab-babad* |
| ... | ... |
| 2147483647 | *luzuz-zuzuz* |
| ... | ... |
| 4294967295 | *zuzuz-zuzuz* |

Please see the article on proquints: http://arXiv.org/html/0901.4016
Original C version: https://github.com/dsw/proquint

To generate a random quint use the `NewQuint()` method:

```c#
var q = Quint32.NewQuint();
```

You can get the string by using `ToString()` method or casting to `string`:
```c#
var s1 = (string)q;
var s2 = q.ToString();
```

You can get the underlying value casting to `uint`:
```c#
uint i = (uint)q;
```

You can use the `>`, `<`, `>=`, `<=`, `==` and `!=` operators to compare the underlying value between two proquints.


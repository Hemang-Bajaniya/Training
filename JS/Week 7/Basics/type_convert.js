console.log("2" - "3");

/*
Numeric conversion rules:
Value 	Becomes…
undefined 	NaN
null 	0
true and false 	1 and 0
string 	Whitespaces (includes spaces, tabs \t, newlines \n etc.) from the start and end are removed. If the remaining string is empty, the result is 0. Otherwise, the number is “read” from the string. An error gives NaN.
*/

/* bool conver
0, null, undefined, NaN, "" 	false
any other value 	true
*/

console.log(Boolean(""), Boolean("0"));
console.log(Boolean(1), Boolean(0), Boolean(NaN), Boolean(undefined), Boolean(Infinity));

let a = 100, b = 20, c = 30;

let maxNumber = (a > b) ? ((a > c) ? a : c) : ((b > c) ? b : c);

console.log(maxNumber);


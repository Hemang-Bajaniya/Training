// 'use strict';

let single = 'single-quoted';
let double = "double-quoted";

let backticks = `backticks`;

let guestList = `Guests:
 * John
 * Pete
 * Mary
`;

console.log(guestList); // a list of guests, multiple lines

console.log('I\'m the string');

// len property
console.log(single.length);

// accessing char
// [+ve index only] | .at(idx) +ve/-ve(reverse)
console.log(single[0], single.at(0));
console.log(single[single.length - 1], single.at(-1));

for (const element of single) {
    console.log(element);
}

// immutable
// err in strict mode
single[0] = 'S';
console.log(single['1']);

console.log(Object.getOwnPropertyDescriptors(single));
// {
//     '0': {
//          value: 's',
//          writable: false,
//          enumerable: true,
//          configurable: false;
//     }
// }

console.log('Interface'.toUpperCase()); // INTERFACE
console.log('Interface'.toLowerCase()); // interface

let str = 'Widget with id';

console.log(str.indexOf('Widget')); // 0, because 'Widget' is found at the beginning
console.log(str.indexOf('widget')); // -1, not found, the search is case-sensitive

console.log(str.indexOf("id")); // 1, "id" is found at the position 1 (..idget with id)
console.log(str.indexOf("id", 4)); // 12, search from pos

// to find all occur
str = "As sly as a fox, as strong as an ox";
let target = "as";
let pos = -1;

while ((pos = str.indexOf(target, pos + 1)) != -1) {
    console.log(`${target} found at index ${pos}`);
}

// search from last
let [firstOccur, lastOccur] = [str.indexOf(target), str.lastIndexOf(target)];
// let [firstOccur, lastOccur] = [str.indexOf(target), str.lastIndexOf(target, 10)];
console.log(`${target} first found at index ${firstOccur} and last at index ${lastOccur}`);

console.log("Widget with id".includes("Widget")); // true
console.log("Hello".includes("Bye")); // false

console.log("Widget".includes("id")); // true
console.log("Widget".includes("id", 3)); // false, from position 3 there is no "id"

console.log("Widget".startsWith("Wid")); // true, "Widget" starts with "Wid"
console.log("Widget".endsWith("get")); // true, "Widget" ends with "get"

// substrings

// 1. slice(start [, end])
// end >= start;
str = "stringify";
console.log(str.slice(0, 5)); // 'strin', the substring from 0 to 5 (not including 5)
console.log(str.slice(0, 1)); // 's', from 0 to 1, but not including 1, so only character at 0

console.log(str.slice(2)); // 'ringify', from the 2nd position till the end
console.log(str.slice(-4, -1));

console.log(str.slice(7, 3));

// 2. str.substring(start [, end])
// end <= start, start <= end
// auto small val = start, high val = end
console.log(str.substring(7, 3));

// not support -ve index
// console.log(str.substring(-4, -1));

// 3. str.substr(start [, length])
// deprecated 
console.log(str.substr(3, 10));
console.log(str.substr(-4, 4));

// Comparing strings
console.log('a' > 'Z'); // true

// different case letters have different codes
console.log("Zootpia".codePointAt(this.length - 1)); // 90
console.log("z".codePointAt(0)); // 122
console.log("z".codePointAt(0).toString(16)); // 7a (if we need a hexadecimal value)

console.log(String.fromCodePoint(90)); // Z
console.log(String.fromCodePoint(0x5a)); // Z (we can also use a hex value as an argument

console.log('Österreich'.localeCompare('Zealand')); // -1







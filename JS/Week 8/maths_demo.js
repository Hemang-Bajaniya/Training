// Math object namespace works with number

// Static properties
console.table(Object.getOwnPropertyDescriptors(Math));


console.log(Math.E);
console.log(Math.LN10);
console.log(Math.PI);
console.log(Math.SQRT2);
console.log(Math.SQRT2);


// Static methods
console.log('\n\nStatic methods');
console.log(Math.abs(3123 - 132312));

const circleArea = Math.PI * 10 * 10;
console.log(circleArea);

console.log(Math.round(4.5));  // 5
console.log(Math.round(4.4));  // 4

console.log(Math.ceil(4.1));  // 5

console.log(Math.floor(4.9)); // 4

console.log(Math.trunc(4.9)); // 4

console.log(Math.pow(2, 3)); // 8

console.log(Math.abs(-10)); // 10

console.table([Math.sign(-5), Math.sign(0), Math.sign(5)]);

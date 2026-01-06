let marks = [45, 78, 92, 30, 65, "10"];

/* function returns:
    passed -> marks > 50
    failed -> marks < 50
*/
const calculateStatus = (mark) => {
    // check for type number
    if (typeof (mark) == 'number')
        return mark > 50 ? "Pass" : "Failed";

    // throw err if marks in invlid format
    throw new Error("Input marks is not a number");
};

for (let index = 0; index < marks.length; index++) {
    const element = marks[index];
    const status = calculateStatus(element);
    console.log(`Mraks of student ${index + 1}:${element}, Status:${status}`);
}
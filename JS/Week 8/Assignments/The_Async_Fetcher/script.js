const user_list = document.getElementById('user_list');

const addUserCard = (data) => {
    const { name } = data;

    let li = document.createElement('li');
    li.classList.add('user_card');
    li.textContent = name;

    user_list.appendChild(li);
};

const loadUserData = async () => {
    try {
        let data = await fetch('https://jsonplaceholder.typicode.com/users');

        if (!data.ok)
            throw new Error('Failed to fetch user data');

        data = await data.json();

        console.log(data, data instanceof Array);

        document.querySelector('#loading_message').style.display = 'none';

        data.forEach(element => {
            addUserCard(element);
        });
    } catch (error) {
        document.querySelector('#loading_message').textContent = error.message;
    }
};

loadUserData();
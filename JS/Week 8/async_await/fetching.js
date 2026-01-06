fetch('https://jsonplaceholder.typicode.com/todos/10')
    .then(response => response.json())
    .then(todo => {
        let article = document.createElement('article');
        article.textContent = `User id:${todo.userId}, title:${todo.title}, iscompleted:${todo.completed}`;
        // article.style.background = 'lightblue';
        article.style.color = 'orangered';

        document.body.appendChild(article);
    })
    .catch(err => console.log(err.message));


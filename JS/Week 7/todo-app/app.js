(function () {
    const form = document.getElementById('todo-form');
    const input = document.getElementById('todo-input');
    const list = document.getElementById('todos');
    const STORAGE_KEY = 'minimal_todos_v1';

    let todos = loadTodos();

    function saveTodos() {
        localStorage.setItem(STORAGE_KEY, JSON.stringify(todos));
    }

    function loadTodos() {
        try {
            const raw = localStorage.getItem(STORAGE_KEY);
            return raw ? JSON.parse(raw) : [];
        } catch (e) {
            console.error('Failed to load todos', e);
            return [];
        }
    }

    function createTodoElement(todo) {
        const el = document.createElement('article');
        el.className = 'todo' + (todo.completed ? ' completed' : '');
        el.dataset.id = todo.id;

        const text = document.createElement('div');
        text.className = 'text';
        text.textContent = todo.text;

        const meta = document.createElement('div');
        meta.className = 'meta';

        const del = document.createElement('button');
        del.className = 'btn-delete';
        del.type = 'button';
        del.setAttribute('aria-label', 'Delete todo');
        del.textContent = '✕';

        del.addEventListener('click', (e) => {
            e.stopPropagation();
            deleteTodo(todo.id);
        });

        el.addEventListener('dblclick', () => {
            toggleTodo(todo.id);
        });

        meta.appendChild(del);
        el.appendChild(text);
        el.appendChild(meta);

        return el;
    }

    function render() {
        list.innerHTML = '';
        if (todos.length === 0) {
            const p = document.createElement('p');
            p.style.color = '#6b7280';
            p.textContent = 'No todos yet — add one above.';
            list.appendChild(p);
            return;
        }

        // show newest first
        const ordered = [...todos].reverse();
        ordered.forEach(t => list.appendChild(createTodoElement(t)));
    }

    function addTodo(text) {
        const trimmed = text.trim();
        if (!trimmed) return;
        const todo = { id: Date.now().toString(36) + Math.random().toString(36).slice(2, 6), text: trimmed, completed: false };
        todos.push(todo);
        saveTodos();
        render();
    }

    function deleteTodo(id) {
        todos = todos.filter(t => t.id !== id);
        saveTodos();
        render();
    }

    function toggleTodo(id) {
        todos = todos.map(t => t.id === id ? { ...t, completed: !t.completed } : t);
        saveTodos();
        render();
    }

    form.addEventListener('submit', (e) => {
        e.preventDefault();
        if (!input.value.trim()) return;
        addTodo(input.value);
        input.value = '';
        input.focus();
    });

    // keyboard: Enter to add (already form), Escape to clear input
    input.addEventListener('keydown', (e) => {
        if (e.key === 'Escape') input.value = '';
    });

    // initial render
    render();
})();
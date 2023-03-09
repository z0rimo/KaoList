const agreeAll = document.querySelector<HTMLInputElement>('input[type="checkbox"].agree-all');
const items = Array.prototype.slice.call(document.querySelectorAll<HTMLInputElement>('input[type="checkbox"].agree-item')) as HTMLInputElement[];
let requiredItems = items.filter((input) => input.hasAttribute('required'));
let submit = document.querySelector<HTMLInputElement | HTMLButtonElement>("*[type=submit]");

function updateCheck() {
    let agreeAllCheck = requiredItems.reduce((acc, current) => acc && current.checked, true);
    submit.disabled = !agreeAllCheck;
    agreeAll.checked = agreeAllCheck;
}

requiredItems.forEach(item => {
    item.onchange = updateCheck;
});

agreeAll.onchange = (evt) => {
    let target = evt.currentTarget as HTMLInputElement;
    requiredItems.forEach(item => {
        item.checked = target.checked;
    });
    updateCheck();
};
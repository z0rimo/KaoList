const agreeAll = document.querySelector<HTMLInputElement>('input[type="checkbox"].agree-all');
const items = Array.prototype.slice.call(document.querySelectorAll<HTMLInputElement>('input[type="checkbox"].agree-item')) as HTMLInputElement[];
const requiredItems = items.filter((input) => input.hasAttribute('required'));

function updateCheck() {
    const agreeAllCheck = requiredItems.reduce((acc, current) => acc && current.checked, true);

    agreeAll.checked = agreeAllCheck;
}

requiredItems.forEach(item => {
    item.onchange = updateCheck;
});

agreeAll.onchange = (evt) => {
    const target = evt.currentTarget as HTMLInputElement;
    requiredItems.forEach(item => {
        item.checked = target.checked;
    });
    updateCheck();
};

window.addEventListener("load", function () {
    updateCheck();
});
document.addEventListener('DOMContentLoaded', () => {
  const checkboxes = document.querySelectorAll('input[type=checkbox]');
  const agreeCheckbox = document.getElementById('agree') as HTMLInputElement;
  const confirmButton = document.getElementById('confirmButton') as HTMLButtonElement;
  const reasonCheckboxes = Array.prototype.slice.call(document.querySelectorAll('input[type=checkbox]:not(#agree)'));

  const otherReasonCheckbox = document.getElementById('reason5') as HTMLInputElement;
  const otherReasonTextarea = document.getElementById('otherReason') as HTMLTextAreaElement;

  const toggleOtherReasonTextarea = () => {
    otherReasonTextarea.disabled = !otherReasonCheckbox.checked;
    if (!otherReasonCheckbox.checked) {
      otherReasonTextarea.value = '';
    }
  };

  const updateButtonState = () => {
    const isAnyReasonChecked = Array.prototype.slice.call(reasonCheckboxes).some(function (checkbox: HTMLInputElement) {
      return checkbox.checked;
    });

    const isAgreeChecked = agreeCheckbox.checked;

    confirmButton.disabled = !(isAnyReasonChecked && isAgreeChecked);
  };

  checkboxes.forEach(checkbox => checkbox.addEventListener('change', () => {
    updateButtonState();
    toggleOtherReasonTextarea();
  }));

  updateButtonState();
  toggleOtherReasonTextarea();
});

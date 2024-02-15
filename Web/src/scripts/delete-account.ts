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
    // const isAnyCheckboxChecked = Array.prototype.slice.call(checkboxes).some((checkbox: HTMLInputElement) => checkbox.checked);
    // const isAgreeChecked = agreeCheckbox.checked;
    // confirmButton.disabled = !(isAnyCheckboxChecked && isAgreeChecked);

    // 이유(reason) 중 하나라도 체크되어 있는지 확인
    var isAnyReasonChecked = Array.prototype.slice.call(reasonCheckboxes).some(function (checkbox: HTMLInputElement) {
      return checkbox.checked;
    });
    // 동의 체크박스가 체크되어 있는지 확인
    var isAgreeChecked = agreeCheckbox.checked;
    // 이유와 동의 체크박스 모두 체크되어 있어야 버튼 활성화
    confirmButton.disabled = !(isAnyReasonChecked && isAgreeChecked);
  };

  checkboxes.forEach(checkbox => checkbox.addEventListener('change', () => {
    updateButtonState();
    toggleOtherReasonTextarea();
  }));

  updateButtonState();
  toggleOtherReasonTextarea();
});

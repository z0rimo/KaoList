document.addEventListener("DOMContentLoaded", function () {
    const messageElement = document.getElementById('server-message');
    if (messageElement && messageElement.dataset.message) {
        const message = messageElement.dataset.message;
        alert(message);

        if (message == "비밀번호가 성공적으로 변경되었습니다.") {
            window.location.href = '/mypage'
        }
    }
});

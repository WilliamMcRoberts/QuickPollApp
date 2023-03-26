var prevScrollPos = window.pageYOffset;

window.onscroll = () => {
    checkCard();
//    getYOffset();
}

//function getYOffset() {
//    const para = document.querySelector('.para');
//    para.textContent = window.pageYOffset;
//}

function checkCard() {

    const cons = document.querySelectorAll('.con')

    const triggerBottom = window.innerHeight;

    cons.forEach(con => {

        if (con.getBoundingClientRect().top < triggerBottom) {
            con.classList.add('fly')
            con.classList.remove('no-fly')
            return
        }
        con.classList.remove('fly')
        con.classList.add('no-fly')
    })
}

function clipboardCopy(text) {
    navigator.clipboard.writeText(text)
    return true;
}
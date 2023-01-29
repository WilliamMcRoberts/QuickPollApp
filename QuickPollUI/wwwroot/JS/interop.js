
window.onscroll = () => {
    checkCard();
}

function checkCard() {

    const cons = document.querySelectorAll('.con')

    const triggerBottom = window.innerHeight / 5 * 5

    cons.forEach(con => {

        const conTop = con.getBoundingClientRect().top

        if (conTop < triggerBottom) {
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
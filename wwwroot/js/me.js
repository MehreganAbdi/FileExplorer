
function downloadFile(path,filename) {
    const download = () => {
        const link = document.createElement('a')

        link.href = path
        link.download = filename
        link.style.display = 'none'

        document.body.appendChild(link)

        link.click()

        document.body.removeChild(link)
    }

    const btn = document.getElementById('btn')

    btn.addEventListener('click', () => {
        download('./' + filename, filename)
    })
}

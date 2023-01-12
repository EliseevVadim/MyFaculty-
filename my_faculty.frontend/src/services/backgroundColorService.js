export default new class BackgroundColorService {
	setBackgroundForErrorPage() {
		document.getElementsByClassName('v-main__wrap')[0].style.background = '#313131';
	}

	restoreBackground() {
		document.getElementsByClassName('v-main__wrap')[0].style.removeProperty('background');
	}
}
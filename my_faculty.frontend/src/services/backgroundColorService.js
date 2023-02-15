export default new class BackgroundColorService {
	setBackgroundForErrorPage() {
		document.querySelector('.v-main__wrap>.container').style.background = '#313131';
	}

	restoreBackground() {
		document.querySelector('.v-main__wrap>.container').style.removeProperty('background');
	}
}
export let config = {
	apiUrl : 'https://localhost:44317',
	headers: {
		'Authorization': 'Bearer ' + localStorage.getItem('apiKey')
	}
};
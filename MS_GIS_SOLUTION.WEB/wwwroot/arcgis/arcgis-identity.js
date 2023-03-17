async function sigInToArcgisServer(){
    try {
        const [esriId, esriConfig] = await loadEsriModules(['esri/identity/IdentityManager', 'esri/config']);

        const formData = new FormData();
        formData.append('username', 'gisadmin');
        formData.append('password', 'sDkjfnsdpjfn.9');
        formData.append('f', 'JSON');
        formData.append('referer', 'https://www.arcgis.com');

        const res = await axios.post('https://datumglb.com/portal/sharing/rest/generateToken', formData);

        await esriId.registerToken({
            expires: res.data.expires,
            server: 'https://datumglb.com/arcgis/rest/services/AzMTS_v7/',
            token: res.data.token
        });

        esriConfig.request.interceptors.push({
            urls: /MapServer\/\d+$/,
            after: function (response) {
                response.data.supportedQueryFormats = "JSON";
            }
        });
    } catch (err) {

    }
}
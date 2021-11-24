export default class Http {
    public static createApiPath(controller: string, action: string, ...ids: string[]): string {
        let baseUrl = `http://localhost:5000/api/${controller}`;
        if (action.length !== 0) {
            baseUrl += `/${action}`;
        }
        for (let i = 0; i < ids.length; i++) {
            baseUrl += `/${ids[i]}`;
        }
        return baseUrl;
    }
}
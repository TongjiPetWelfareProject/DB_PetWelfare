import axios from 'axios';

export default {
    //获取公告ID，标题和内容
    bulletinAPI() {
        console.log("发出公告请求");
        return axios
            .get('/api/bulletin')
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取公告数据时出错：' + error.message);
            });
    },
};

export const getPosts = async () => {
    try {  
        const response = await axios.get('/api/posts');
        return response.data;
     } catch (error) {
            console.error('获取帖子列表时出错:', error);
            throw error;  
        }
};
export const searchPosts = async(searchText)=> {
    try {
    const response = await axios.get('/api/posts-search', { params: { searchText } });
     return response.data; } 
     catch (error) {
     console.error('搜索帖子时出错:', error);
     throw error;
    }
};
                
                

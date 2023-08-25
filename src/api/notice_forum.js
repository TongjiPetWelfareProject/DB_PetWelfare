import axios from 'axios';

export default {
    //获取公告ID，标题和内容
    bulletinAPI() {
        return axios
            .get('/api/bulletin')
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取公告数据时出错：' + error.message);
            });
    },

    getposts() {
        return axios
            .get('/api/forum')
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取帖子数据时出错：' + error.message);
            });
    },

    getpost(post_id) {
        return axios
            .post('/api/post',{post_id})
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取帖子数据时出错：' + error.message);
            });
    },

    postcontent(user_id,post_title,post_content) {
        console.log({user_id,post_title,post_content})
        return axios
            .post('/api/postcontent',{user_id,post_title,post_content})
            .then((response) => response.data)
            .catch(error => {
                throw new Error('发帖出错：' + error.message);
            })
    }
};

// export const searchPosts = async(searchText)=> {
//     try {
//     const response = await axios.get('/api/posts-search', { params: { searchText } });
//      return response.data; } 
//      catch (error) {
//      console.error('搜索帖子时出错:', error);
//      throw error;
//     }
// };

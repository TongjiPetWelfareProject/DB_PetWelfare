import axiosInstance from '@/util/token-config';
import axios from 'axios';

export default {
    //获取公告ID，标题和内容
    bulletinAPI() {
        return axiosInstance
            .get('/api/bulletin')
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取公告数据时出错：' + error.message);
            });
    },

    getposts() {
        return axiosInstance
            .get('/api/forum')
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取帖子数据时出错：' + error.message);
            });
    },

    getpost(post_id) {
        return axiosInstance
            .post('/api/post',{post_id})
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取帖子数据时出错：' + error.message);
            });
    },

    likepost(user_id,post_id) {
        return axiosInstance
        .post('/api/likepost',{user_id,post_id})
        .then((response) => response.data)
        .catch(error => {
            throw new Error('点赞帖子数据时出错：' + error.message);
        });
    },

    deletepost(post_id,user_id) {
        return axiosInstance
        .post('/api/deletepost',{post_id,user_id})
        .then((response) => response.data)
        .catch(error => {
            throw new Error('删除帖子数据时出错：' + error.message);
        });
    },

    iflike(user_id,post_id) {
        return axiosInstance
        .post('/api/iflikepost',{user_id,post_id})
        .then((response) => response.data)
        .catch(error => {
            throw new Error('点赞帖子数据时出错：' + error.message);
        });
    },

    getcomment(post_id) {
        return axiosInstance
            .post('/api/postcomment',{post_id})
            .then((response) => response.data)
            .catch(error => {
                throw new Error('获取帖子数据时出错：' + error.message);
            });
    },

    addcomment(user_id,post_id,added_comment) {
        return axiosInstance
            .post('/api/addcomment',{user_id,post_id,added_comment})
            .then((response) => response.data)
            .catch(error => {
                throw new Error('评论时出错：' + error.message);
            })
    },

    deletecomment(user_id,post_id,comment_time) {
        return axiosInstance
            .post('/api/deletecomment',{user_id,post_id,comment_time})
            .then((response) => response.data)
            .catch(error => {
                throw new Error('评论时出错：' + error.message);
            })
    },

    postcontent(user_id,post_title,post_content,param) {
        return axiosInstance
            .post('/api/postcontent',{user_id,post_title,post_content,param})
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

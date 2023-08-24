// api.js
import axios from 'axios';

export default{
  getPetList() {
    return axios.get('/api/petlist')
      .then(response => {
        return response.data;
      })
      .catch(error => {
        throw error;
      });
  },
  
  getPetDetails(petId) {
    return axios.get(`/api/pet-details/${petId}`)
      .then(response => {
        return response.data; // Adjust this according to your backend response structure
      })
      .catch(error => {
        throw error;
      });
  },

  submitAdoptApplication(userInfo, petInfo, formData) {//export可以将对象暴露给其他模块使用
    const adoptData = {
      user: userInfo.User_ID,
      pet: petInfo.Pet_ID,
      gender: formData.gender,
      pet_exp: formData.pet_exp,
      long_term_care: formData.long_term_care,
      w_to_treat: formData.w_to_treat,
      d_care_h: formData.d_care_h,
      P_caregiver: formData.P_caregiver,
      f_popul: formData.f_popul,
      be_children: formData.be_children,
      accept_vis: formData.accept_vis,
    };
  
    return axios.post('/api/pet-adopt', adoptData)//指后端服务器的pet-adopt接口
      .then(response => {
        return response.data; // You might want to return the response data here
      })
      .catch(error => {
        throw error;
      });
  },

  submitLike(userInfo, petInfo, likes) {
    const likeData = {
      user: userInfo.User_ID,
      pet: petInfo.Pet_ID,
      likes: likes,
    };
  
    return axios.post('/api/pet-like', likeData)  // 假设后端有名为 pet-like 的接口来处理点赞
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  submitFavorite(userInfo, petInfo, favorites) {
    const favoriteData = {
      user: userInfo.User_ID,
      pet: petInfo.Pet_ID,
      favorites: favorites,
    };
  
    return axios.post('/api/pet-favorite', favoriteData)  // 假设后端有名为 pet-favorite 的接口来处理收藏请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  submitReading(userInfo, petInfo) {
    const readingData = {
      user: userInfo.User_ID,
      pet: petInfo.Pet_ID,
    };
  
    return axios.post('/api/pet-reading', readingData)  // 假设后端有名为 pet-reading 的接口来处理阅读请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  submitComment(userInfo, petInfo) {
    const commentData = {
      user: userInfo.User_ID,
      pet: petInfo.Pet_ID,
      text: commentText,
      time: commentTime
    };
  
    return axios.post('/api/pet-comment', commentData)  // 假设后端有名为 pet-comment 的接口来处理评论请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  deleteComment(commentInfo) {
    const commentData = {
      comment: commentInfo.comment_ID,
    };
  
    return axios.post('/api/pet-comment', commentData)  // 假设后端有名为 pet-comment 的接口来处理删除评论请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },
}


// api.js
import axios from 'axios';

export default{
  getPetList() {
    return axios.get('/api/pet-adopt-list')
      .then(response => {
        return response.data;
      })
      .catch(error => {
        throw error;
      });
  },

  getPetDetails(PID) {
    return axios.get(`/api/pet-details/${PID}`)
      .then(response => {
        return response.data; // Adjust this according to your backend response structure
      })
      .catch(error => {
        throw error;
      });
  },

  ifFavorite(UID, PID) {
    return axios.post('/api/iffavoritepet',{UID, PID})
      .then((response) => response.data)
      .catch(error => {
          throw new Error('获取用户对宠物点赞数据时出错：' + error.message);
      });
  },

  submitFavorite(UID, PID) {
    const submitFavoriteData = {
      user: UID,
      pet: PID,
    };
  
    return axios.post('/api/pet-submit-favorite', submitFavoriteData)  // 假设后端有名为 pet-favorite 的接口来处理收藏请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw new Error('提交点赞数据时出错：' + error.message);
      });
  },

  cancelFavorite(UID, PID) {
    const cancelFavoriteData = {
      user: UID,
      pet: PID,
    };
  
    return axios.post('/api/pet-cancel-favorite', cancelFavoriteData)  // 假设后端有名为 pet-favorite 的接口来处理收藏请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw new Error('提交取消点赞数据时出错：' + error.message);
      });
  },

  submitReading(UID, PID) {
    const submitReadingData = {
      user: UID,
      pet: PID,
    };
  
    return axios.post('/api/pet-reading', submitReadingData)  // 假设后端有名为 pet-reading 的接口来处理阅读请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  ifLike(UID, PID) {
    return axios.post('/api/iflikepet',{UID, PID})
      .then((response) => response.data)
      .catch(error => {
          throw error;
      });
  },

  submitLike(UID, PID) {
    const submitLikeData = {
      user: UID,
      pet: PID,
    };
  
    return axios.post('/api/pet-submit-like', submitLikeData)  // 假设后端有名为 pet-like 的接口来处理点赞
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  cancelLike(UID, PID) {
    const cancelLikeData = {
      user: UID,
      pet: PID,
    };
  
    return axios.post('/api/pet-cancel-like', cancelLikeData)  // 假设后端有名为 pet-like 的接口来处理点赞
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  submitAdoptApplication (UID, PID, formData) {//export可以将对象暴露给其他模块使用
    const adoptData = {
      user: UID,
      pet: PID,
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

  getComment(PID) {
    return axios.get(`/api/comment-list/${PID}`)
    .then(response => {
      return response.data;
    })
    .catch(error => {
      throw error;
    });
  },


  submitComment(UID, PID, commentText) {
    const submitCommentData = {
      user: UID,
      pet: PID,
      commentText: commentText,
    };
  
    return axios.post('/api/pet-submit-comment', submitCommentData)  // 假设后端有名为 pet-comment 的接口来处理评论请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },

  deleteComment(CID) {
    const commentData = {
      comment: CID,
    };
  
    return axios.post('/api/pet-delete-comment', commentData)  // 假设后端有名为 pet-comment 的接口来处理删除评论请求
      .then(response => {
        return response.data; // 你可能希望在这里返回响应数据
      })
      .catch(error => {
        throw error;
      });
  },
}


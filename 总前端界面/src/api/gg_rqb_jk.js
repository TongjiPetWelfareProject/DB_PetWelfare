import axios from 'axios';

export default {
//获取全部公告
  getNoticeAPI() {
    return axios
      .get('/api/notice')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取公告数据时出错：' + error.message);
      });
  },
  //获取捐赠记录，捐赠id，用户id，时间，金额
  getDonationAPI() {
    return axios
      .get('/api/donation')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取捐款记录数据时出错：' + error.message);
      });
  },
};

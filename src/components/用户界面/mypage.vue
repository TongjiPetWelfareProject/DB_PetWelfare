<template>
  <div style="display: flex;align-items: center;margin-left: 5vw;">
        <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
        &nbsp;<a href="\" style="text-decoration: none;color:#538adc;">返回主页</a>
  </div>
  <div class="bigcontainer">

  <div class="common-layout">
    <el-container>
      <el-header style="height: 200px;">
        <div class="avatar-container">
          <label for="avatar-upload">
            <img id="avatar-img" class="avatar" :src="userStore.userInfo.Avatar">
            <input type="file" id="avatar-upload" accept="image/*" @change="submitAvatar">
          </label>
          <img class="background-image" src="@/photos/mypagepet.jpg">
        </div>
       
        <el-descriptions class="margin-top" :column="3" :size="size" border>
    <template #extra>
      <el-button type="primary" @click="dialogFormVisible = true">编辑</el-button>

      <el-dialog v-model="dialogFormVisible" title="修改个人信息">
        <el-form :model="editedform">
          <el-form-item label="用户名" :label-width="formLabelWidth">
            <el-input :placeholder="infoform.username" v-model="editedform.name" autocomplete="off" />
          </el-form-item>
          <el-form-item label="电话" :label-width="formLabelWidth">
            <el-input :placeholder="infoform.phone" v-model="editedform.phone" autocomplete="off" @input="handlePhoneInput"/>
          </el-form-item>
          <el-form-item label="地址" :label-width="formLabelWidth">
            <div class="form-group">
              <select v-model="selectedProvince" class="province-select">
                <option v-for="(key,value) in provinces" :value="key" :key="key">{{ value }}</option>
              </select>
              <select v-model="selectedCity" name="cities" class="city-select">
                  <option v-for="city in cities" :value="city.value" :key="city.key">{{ city.key }}</option>
              </select>
            </div>
          </el-form-item>
        </el-form>
        <template #footer>
          <span class="dialog-footer">
            <el-button @click="dialogFormVisible = false">取消</el-button>
            <el-button type="primary" :disabled="!editedform.name || !editedform.phone || !selectedProvince || !selectedCity" @click="editInfo">确认</el-button>
          </span>
        </template>
      </el-dialog>
    </template>
    <el-descriptions-item>
      <template #label><div class="cell-item"><el-icon :style="iconStyle"><user/></el-icon>用户名</div></template>
      {{ infoform.username }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label><div class="cell-item"><el-icon :style="iconStyle"><iphone/></el-icon>Telephone</div></template>
      {{ infoform.phone }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label><div class="cell-item"><el-icon :style="iconStyle"><star/></el-icon>点赞量</div></template>
      {{ infoform.like_num }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label><div class="cell-item"><el-icon :style="iconStyle"><tickets/></el-icon>阅读量</div></template>
      {{ infoform.read_num }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label><div class="cell-item"><el-icon :style="iconStyle"><office-building/></el-icon>地址</div></template>
      {{ infoform.address }}
    </el-descriptions-item>
  </el-descriptions>
      </el-header>
      <el-main>
        <el-tabs v-model="activeName" class="demo-tabs" @tab-click="handleClick" type="border-card">
        <el-tab-pane label="我的宠物" name="first">
          <el-tabs tab-position="left"  class="demo-tabs">
            <el-tab-pane label="我的点赞">
              <div class="donatecardcontainer" style="gap:40px">
                  <el-card v-for="lplist in likedpetlist" :body-style="{ padding: '0px' }" style="width: 300px;height:400px" @click="goToPet(lplist.PET_ID)">
                      <img src="../../../public/home5.jpg" class="mypagepetimage">
                      <div style="padding: 14px;display: flex;
                        justify-content: center;
                        flex-direction: column; ">
                        <span style="font-size: 40px; color:#edb055;font-weight: bold;">{{ lplist.PET_NAME }}</span>
                        <br>
                        <span style="font-size: 18px">{{ lplist.SEX }}</span>
                        <span style="font-size: 16px; color:#6b6a68">{{ lplist.AGE }}</span>
                        <br>
                      </div>
                      <!-- <router-link :to="{ name: 'pet_details', params: { id: pet.id } }" style=" text-decoration: none;
                      color:#edb055 ;">
                        查看详情  —>
                      </router-link> -->
                    </el-card>
                </div>
            </el-tab-pane>
            <el-tab-pane label="我的收藏">
              <div class="donatecardcontainer" style="gap:40px">
                <el-card v-for="cplist in collectedpetlist" :body-style="{ padding: '0px' }" style="width: 300px;height:400px" @click="goToPet(cplist.PET_ID)">
                      <img src="../../../public/home5.jpg" class="mypagepetimage">
                      <div style="padding: 14px;display: flex;
                        justify-content: center;
                        flex-direction: column; ">
                        <span style="font-size: 40px; color:#edb055;font-weight: bold;">{{cplist.PET_NAME}}</span>
                        <br>
                        <span style="font-size: 18px">{{cplist.SEX}}</span>
                        <span style="font-size: 16px; color:#6b6a68">{{cplist.AGE}}</span>
                        <br>
                      </div>
                      <!-- <router-link :to="{ name: 'pet_details', params: { id: pet.id } }" style=" text-decoration: none;
                      color:#edb055 ;">
                        查看详情  —>
                      </router-link> -->
                    </el-card>
                </div>
            </el-tab-pane>
            <el-tab-pane label="我的评论">
              <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="$router.push('/post_details')"> -->
                  <el-card v-for="cmplist in commentedpetlist" class="mypage-card" shadow="always" >
                    <template #header>
                      <div class="mypagecard-header">
                        <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center; margin-left: 5px;margin-top:-10px">{{cmplist.PET_NAME}}</span>
                      </div>
                    </template>
                    <div class="mypagecardbody">
                      <div style="display: flex; align-items: center; margin-left: 5px;font-size: 15px;">
                        <img src="@/photos/头像.jpg" style="width: 30px; height: 30px; border-radius: 50%;">
                        <span style="margin-left: 5px;">{{cmplist.USER_NAME}}:</span>
                        <span>{{cmplist.CONTENTS}}</span>
                      </div>            
                      <div class="mypagecardtime">{{cmplist.TIME}}</div>
                      <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
                    </div>
                  </el-card>
              <!-- </li> -->
              
            </el-tab-pane>
            <el-tab-pane label="我的领养">
              <div class="donatecardcontainer" style="gap:40px">
                <el-card v-for="adlist in adoptedpetlist" :body-style="{ padding: '0px' }" style="width: 300px;height:400px" @click="goToPet(adlist.PET_ID)">
                      <img src="../../../public/home5.jpg" class="mypagepetimage">
                      <div style="padding: 14px;display: flex;
                        justify-content: center;
                        flex-direction: column; ">
                        <div style="display:flex;text-align: center;align-items: center;justify-content: space-between;">
                          <span style="font-size: 40px; color:#edb055;font-weight: bold;">{{adlist.PET_NAME}}</span>
                           <span style="font-size: 24px;font-weight: bold;color:#eb7543">{{adlist.STATE}}</span>
                        </div>
                       
                        <br>
                        <span style="font-size: 18px;color:#393939">{{adlist.SEX}}</span>
                        <span style="font-size: 16px; color:#6b6a68">{{adlist.AGE}}</span>
                        <br>
                      </div>
                      <!-- <router-link :to="{ name: 'pet_details', params: { id: pet.id } }" style=" text-decoration: none;
                      color:#edb055 ;">
                        查看详情  —>
                      </router-link> -->
                    </el-card>
                </div>
            </el-tab-pane>
            <el-tab-pane label="我的寄养">
              <div class="donatecardcontainer">
                <el-card v-for="flist in fosteredpetlist" class="mypagefoster" shadow="always">
                  <template #header>
                    <div class="mypagecard-header">
                      <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center;margin-top:-10px">{{flist.PET_NAME}}</span>
                    </div>
                  </template>
                  <div class="mypagefostertext" style="margin-top:2px">寄养时长：{{ flist.DURATION }}天</div>
                  <div class="mypagefostertext">寄养起始时间：{{ flist.STARTDATE }}</div>
                  <div class="mypagefostertext">寄养费用：{{ flist.EXSPENSE }}</div>
                </el-card>
              </div>
            </el-tab-pane>
          </el-tabs>
        </el-tab-pane>



        <el-tab-pane label="我的论坛" name="second">
          <el-tabs tab-position="left"  class="demo-tabs">
            <el-tab-pane label="我的发帖">
              <div v-for="spo in postsend" :key="spo.post_id" @click="goToPost(spo)">
                <el-card class="post-card" shadow="always" style="margin-top:10px">
                  <div class="post-title">{{spo.heading}}</div>
                  <div class="post-info">
                    <div>发表时间：{{spo.time}}</div>
                    <div>阅读量：{{spo.click}}</div>
                    <div>喜爱数量：{{spo.likenum}}</div>
                    <div>评论数量：{{spo.commentnum}}</div>
                    <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
                  </div>
                </el-card>
              </div>
            </el-tab-pane>
            <el-tab-pane label="我的点赞">
              <div v-for="lpo in postlike" :key="lpo.post_id" @click="goToPost(lpo)">
                <el-card class="post-card" shadow="always" style="margin-top:10px">
                  <div class="post-title">{{lpo.heading}}</div>
                  <div class="post-info">
                    <div>发表时间：{{lpo.time}}</div>
                    <div>阅读量：{{lpo.click}}</div>
                    <div>喜爱数量：{{lpo.likenum}}</div>
                    <div>评论数量：{{lpo.commentnum}}</div>
                    <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
                  </div>
                </el-card>
              </div>
            </el-tab-pane>

            <el-tab-pane label="我的评论">
              <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="$router.push('/post_details')"> -->
                <el-card v-for="postcom in postcomment" class="mypage-card" shadow="always" @click="goToPost(postcom)">
                    <template #header>
                      <div class="mypagecard-header">
                        <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center; margin-left: 5px;margin-top:-10px">{{ postcom.title }}</span>
                      </div>
                    </template>
                    <div class="mypagecardbody">
                      <div style="display: flex; align-items: center; margin-left: 5px;font-size: 15px;">
                        <img src="@/photos/头像.jpg" style="width: 30px; height: 30px; border-radius: 50%;">
                        <span style="margin-left: 5px;">{{ postcom.username }}:</span>
                        <span>{{ postcom.content }}</span>
                      </div>            
                      <div class="mypagecardtime">{{ postcom.time }}</div>
                      <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
                    </div>
                  </el-card>
              <!-- </li> -->
              
            </el-tab-pane>

          </el-tabs>
        </el-tab-pane>


        <el-tab-pane label="其他" name="third">
          <el-tabs tab-position="left"  class="demo-tabs">
            <el-tab-pane label="我的医疗">
              <div class="donatecardcontainer">
                <el-card v-for="medi in medicallist" class="mypagefoster" shadow="always">
                  <template #header>
                    <div class="mypagecard-header">
                      <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center;margin-top:-10px">{{ medi.petname }}</span>
                    </div>
                  </template>
                  <div class="mypagefostertext" style="margin-top:2px">治疗原因：{{ medi.reason }}</div>
                  <div class="mypagefostertext">治疗医生：{{ medi.vetname }}</div>
                  <div class="mypagefostertext">治疗时间：{{ medi.time }}</div>
                </el-card>
              </div>        
            </el-tab-pane>
            <el-tab-pane label="我的捐款">
              <div class="donatecardcontainer">
                <el-card v-for="dona in donations" class="mypagedonate" shadow="always">
                <!-- <template #header>
                  <div class="mypagecard-header">
                    <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center;margin-top:-10px">花花</span>
                  </div>
                </template> -->
                <div class="mypagefostertext" style="margin-top:2px">捐款金额：{{ dona.amount }}</div>
                <div class="mypagefostertext">捐款时间：{{ dona.time }}</div>
              </el-card>
              </div> 
            </el-tab-pane>
          </el-tabs>
        </el-tab-pane>
      </el-tabs>    
      </el-main>
    </el-container>
  </div>
</div>
</template>

<script setup>
import { ref, onMounted,reactive,watch } from 'vue'
import { useUserStore } from '@/store/user'
import userinfo from '@/api/userInfo'
import { computed } from 'vue'
import { Iphone, OfficeBuilding, Tickets, User, Star } from '@element-plus/icons-vue'
import jsonData from '@/components/登录注册/values.json'
import { ElMessage } from 'element-plus'
import { useRouter } from 'vue-router'
import axios from 'axios'
const router = useRouter()
const avatarUrl = ref('@/photos/头像.jpg')

const handleFileChange = (event) => {
  const file = event.target.files[0];
  if (file) {
    avatarUrl.value = URL.createObjectURL(file);
    const formData = new FormData();
    formData.append('avatar', file);
    console.log(file)
    try {
      const response = userinfo.avatarAPI(userStore.userInfo.User_ID,formData)
    } catch (error) {
      console.error('发送头像数据时出错：', error);
    }
  }
}



const provinces = ref(jsonData.provinces)
const selectedProvince = ref('')
const cities = ref([])
const selectedCity = ref('')
const avatar= ref()
const userStore = useUserStore()
const activeName = ref('first')
const infoform = ref({
  username:'',
  phone:'',
  address:'',
  like_num: 0,
  read_num: 0,
});
const postcomment = ref([])
const postlike = ref([])
const postsend = ref([])
const donations = ref([])
const medicallist = ref([])
const collectedpetlist = ref([])
const likedpetlist = ref([])
const commentedpetlist = ref([])
const adoptedpetlist = ref([])
const fosteredpetlist = ref([])
const dialogFormVisible = ref(false)
const formLabelWidth = '140px'
const editedform = reactive({
  name: '',
  region: '',
  date1: '',
  date2: '',
  delivery: false,
  type: [],
  resource: '',
  desc: '',
  phone:''
})

const submitAvatar = async (event) => {
  try {
    let param = new FormData()
    const file = event.target.files[0];
    param.append('user_id', userStore.userInfo.User_ID)
    param.append('filename',file)
    console.log(param)
    await axios({
        method: 'POST',
        url: '/api/editavatar',
        data: param,
        headers: {
            "Content-Type": "multipart/form-data"
        }
    }).then(response => {
        console.log(response.data)
    })


    // 显示成功提示
    ElMessage.success({
      message: '上传成功',
      duration: 1000
    });
    setTimeout(() => {
      location.reload();
    }, 1000);

  } catch (error) {
    console.error('修改失败：', error);

    // 显示失败提示
    ElMessage.error({
      message: '修改失败，错误信息：' + error.message,
      duration: 3000 
    });
  }
}

watch(selectedProvince, (newSelectedProvince) => {
  cities.value = Object.entries(jsonData[newSelectedProvince] || {}).map(([key, value]) => ({
    key,
    value
  }));
});

const handlePhoneInput = () => {
    // 获取输入框的值并移除所有非数字字符
    const digitsOnly = editedform.phone.replace(/\D/g, "");

    // 在第4个和第9个位置插入空格
    const formattedValue = insertSpaces(digitsOnly, [3, 7]);
    console.log(formattedValue)
    editedform.phone = formattedValue; // 更新 newEmployee.value.phone
};

const insertSpaces = (str, positions) => {
    const result = [];
    let positionIndex = 0;

    for (let i = 0; i < str.length; i++) {
        if (positionIndex < positions.length && i === positions[positionIndex]) {
            result.push(" ");
            positionIndex++;
        }
        result.push(str[i]);
    }

    return result.join("");
};

const goToPost = (post) => {
  // 跳转到帖子详情页
  console.log('跳转到帖子详情页：' + post.heading);
  router.push({ path: '/post_details', query: { post_id: post.post_id }});
};

const goToPet = (petId) => {
    // 使用Vue Router进行路由跳转
    router.push({ name: 'pet_details', params: { id: petId } });
};

const editInfo = async () => {
    dialogFormVisible.value = false
    console.log(selectedCity)
    try {
      const response = await userinfo.editInfoAPI(userStore.userInfo.User_ID,editedform.name,editedform.phone,selectedProvince.value,selectedCity.value);
      ElMessage.success({
      message: '修改成功',
      duration: 1000 // 持续显示时间（毫秒）
    });
    userStore.userInfo.User_Name=editedform.name
    userStore.userInfo.Phone_Number=editedform.phone
    userStore.userInfo.Address=selectedProvince.value+selectedCity.value
    // 停顿1秒后刷新
    setTimeout(() => {
      location.reload();
    }, 1000);
    } catch (error) {
      console.error('获取用户帖子评论时出错：', error);
      // 显示失败提示
      ElMessage.error({
      message: '修改失败，错误信息：' + error.message,
      duration: 1000 // 持续显示时间（毫秒）
    });
    }
};

const getUserInfo = async () => {
    try {
      const response = await userinfo.userInfoAPI(userStore.userInfo.User_ID);
      infoform.value.username= response.user_name,
      infoform.value.address= response.address,
      infoform.value.phone= response.phone,
      infoform.value.like_num= response.likes,
      infoform.value.read_num= response.reads,
      infoform.value.avatar= response.avatar,
      userStore.userInfo.User_Name= response.user_name,
      userStore.userInfo.Phone_Number= response.phone,
      userStore.userInfo.Address= response.address,
      userStore.userInfo.Avatar=response.avatar;
      console.log('useravatar'+userStore.userInfo.Avatar)
    } catch (error) {
      console.error('获取用户人气数据时出错：', error);
    }
};

const getUserComment = async () => {
    try {
      const response = await userinfo.userPostCommentAPI(userStore.userInfo.User_ID);
      for(const comment of response ){
        postcomment.value.push({
          title: comment.post_Title,
          avator:'@/photos/头像.jpg',
          username: userStore.userInfo.User_Name,
          content: comment.content,
          time:comment.comment_Time,
          post_id:comment.pid
        })
      }
    } catch (error) {
      console.error('获取用户帖子评论时出错：', error);
    }
};

const getUserLike = async () => {
    try {
      const response = await userinfo.userPostLikeAPI(userStore.userInfo.User_ID);
      for(const like of response ){
        //console.log('帖子点赞'+like.HEADING+like.COMMENT_NUM)
        postlike.value.push({
          post_id: like.POST_ID,
          heading: like.HEADING,
          click: like.READ_COUNT,
          likenum: like.LIKE_NUM,
          commentnum: like.COMMENT_NUM,
          time:like.POST_TIME
        })
      }
    } catch (error) {
      console.error('获取用户帖子点赞时出错：', error);
    }
};

const getUserSend = async () => {
    try {
      const response = await userinfo.userPostSendAPI(userStore.userInfo.User_ID);
      for(const like of response ){
        console.log(like)
        postsend.value.push({
          post_id: like.POST_ID,
          heading: like.HEADING,
          click: like.READ_COUNT,
          likenum: like.LIKE_NUM,
          commentnum: like.COMMENT_NUM,
          time:like.POST_TIME
        })
      }
    } catch (error) {
      console.error('获取用户发帖时出错：', error);
    }
};

const getUserDonation = async () => {
    try {
      const response = await userinfo.userDonationAPI(userStore.userInfo.User_ID);
      for(const donation of response ){
        donations.value.push({
          amount: donation.DONATION_AMOUNT,
          time: donation.DONATION_TIME
        })
      }
    } catch (error) {
      console.error('获取用户捐款数据时出错：', error);
    }
};

const getUserMedical = async () => {
    try {
      const response = await userinfo.userMedicalAPI(userStore.userInfo.User_ID);
      for(const medical of response ){
        medicallist.value.push({
          vetname: medical.VET_NAME,
          petname: medical.PET_NAME,
          time: medical.CUSTOM_TIME,
          reason: medical.REASON
        })
      }
    } catch (error) {
      console.error('获取用户捐款数据时出错：', error);
    }
};

const getUserCollectPets = async () => {
    try {
      const response = await userinfo.userCollectPetsAPI(userStore.userInfo.User_ID);
      for(const pet of response ){
        collectedpetlist.value.push({
          PET_ID: pet.PET_ID,
          PET_NAME: pet.PET_NAME,
          SEX: pet.SEX=='M'?'弟弟':'妹妹',
          AGE: pet.AGE+'岁'
        })
      }
    } catch (error) {
      console.error('获取用户收藏数据时出错：', error);
    }
};

const getUserLikePets = async () => {
    try {
      const response = await userinfo.userLikePetsAPI(userStore.userInfo.User_ID);
      for(const pet of response ){
        likedpetlist.value.push({
          PET_ID: pet.PET_ID,
          PET_NAME: pet.PET_NAME,
          SEX: pet.SEX=='M'?'弟弟':'妹妹',
          AGE: pet.AGE+'岁'
        })
      }
    } catch (error) {
      console.error('获取用户收藏数据时出错：', error);
    }
};

const getUserCommentPets = async () => {
    try {
      const response = await userinfo.userCommentPetsAPI(userStore.userInfo.User_ID);
      for(const pet of response ){
        console.log(pet)
        commentedpetlist.value.push({
          USER_NAME:pet.USER_NAME,
          PET_NAME: pet.PET_NAME,
          TIME: pet.COMMENT_TIME,
          CONTENTS: pet.COMMENT_CONTENTS
        })
      }
    } catch (error) {
      console.error('获取用户收藏数据时出错：', error);
    }
};

const getUserAdoptPets = async () => {
    try {
      const response = await userinfo.userAdoptPetsAPI(userStore.userInfo.User_ID);
      for(const pet of response ){
        adoptedpetlist.value.push({
          PET_ID:pet.PET_ID,
          PET_NAME: pet.PET_NAME,
          SEX: pet.SEX=='M'?'弟弟':'妹妹',
          AGE: pet.AGE+'岁',
          STATE:pet.CENSOR_STATE
        })
      }
    } catch (error) {
      console.error('获取用户领养数据时出错：', error);
    }
};

const getUseRFosterPets = async () => {
    try {
      const response = await userinfo.userFosterPetsAPI(userStore.userInfo.User_ID);
      for(const pet of response ){
        fosteredpetlist.value.push({
          PET_NAME: pet.PET_NAME,
          DURATION: pet.DURATION,
          STARTDATE: pet.START_DATE,
          EXSPENSE:pet.EXPENSE+'.00￥'
        })
      }
    } catch (error) {
      console.error('获取用户寄养数据时出错：', error);
    }
};

onMounted(() => {
    getUserInfo();
    getUserComment();
    getUserLike();
    getUserSend();
    getUserDonation();
    getUserMedical();
    getUserCollectPets();
    getUserLikePets();
    getUserCommentPets();
    getUserAdoptPets();
    getUseRFosterPets();
    
});

const size = ref('')
const iconStyle = computed(() => {
  const marginMap = {
    large: '8px',
    default: '6px',
    small: '4px',
  }
  
  return {
    marginRight: marginMap[size.value] || marginMap.default,
  }
})
</script>

<style scoped>
.post-card{
  cursor: pointer;
}
.avatar-container {
  display: flex;
  align-items: flex-end;
}
.avatar {
  width: 70px;
  height: 70px;
  border-radius: 50%;
  margin-bottom: -60px;
  cursor: pointer;
}
.background-image {
  width: 400px;
  margin-bottom: -60px;
  margin-left: 24vw;
}
input[type="file"] {
  display: none;
}

.bigcontainer{
  /* justify-content: center; */
  width:100%
}

.mypage-card{
  cursor: pointer;
  margin-top: 10px;
}

.mypagecard-header{
  padding-left: 1%;
  height:0px;
  margin-top:-10px
}

.mypagecardbody{
  padding-left: 1%;
  display: flex;
  justify-content: space-between;
  text-align: center;
  min-height:10px
}

.mypagecardtime{
  font-size: 14px;
  color:rgb(109, 109, 109);
  font-weight:lighter
}

.mypagefostertext{
  font-size: 15px;
  color:rgb(89, 89, 89);
  margin-top:5px
}

.mypagefoster{
  width:300px;
  height:150px;
  flex: 0 0 calc(33.33% - 20px); 
}


.donatecardcontainer {
  display: flex;
  flex-wrap: wrap;
  gap: 20px; /* 控制卡片之间的间隔 */
  cursor: pointer;
}

.mypagedonate{
  width:300px;
  height:100px;
  flex: 0 0 calc(33.33% - 20px); /* 每行三张，卡片宽度占比 */
}



.province-select {
  /* 调整下拉选项的宽度 */
  width: 40%;
  /* 调整下拉选项的高度 */
  height: 50%;
  /* 调整下拉选项的字体大小 */
  font-size: 16px;
  /* 调整下拉选项的背景颜色 */
  background-color: #ffffff;
  /* 调整下拉选项的边框样式 */
  border: 1px solid #cccccc;
  /* 调整下拉选项的边框圆角 */
  border-radius: 4px;
  /* 调整下拉选项的内边距 */
  padding: 6px;
  /* 调整下拉选项的颜色 */
  color: #333333;
}
   
.city-select {
  /* 调整下拉选项的宽度 */
  width: 40%;
  /* 调整下拉选项的高度 */
  height: 50%;
  /* 调整下拉选项的字体大小 */
  font-size: 16px;
  /* 调整下拉选项的背景颜色 */
  background-color: #ffffff;
  /* 调整下拉选项的边框样式 */
  border: 1px solid #cccccc;
  /* 调整下拉选项的边框圆角 */
  border-radius: 4px;
  /* 调整下拉选项的内边距 */
  padding: 6px;
  /* 调整下拉选项的颜色 */
  color: #333333;
  margin-left: 5%;
}

.el-descriptions {
  margin-top: 20px;
}
.cell-item {
  display: flex;
  align-items: center;
}
.margin-top {
  margin-top: 20px;
}
.el-row {
  margin-bottom: 5px;
}

.el-row:last-child {
  margin-bottom: 0;
}

.el-col {
  border-radius: 1px;
}

.grid-content {
  border-radius: 4px;
  min-height: 36px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.text {
  font-size: 14px;
}

.item {
  margin-bottom: 18px;
}

.box-card {
  width: 390px;
  min-height:350px;
}

.common-layout {
  /* background-image: url(assets/两只狗勾.jpg);
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat; */
  width:87vw;
  margin-left: 4vw;
}

.demo-type {
  display: flex;
}
.demo-type > div {
  flex: 1;
  text-align: center;
}

.demo-type > div:not(:last-child) {
  border-right: 1px solid var(--el-border-color);
}



  .infinite-list {
  height: 300px;
  padding: 0;
  margin: 0;
  list-style: none;
}
.infinite-list .infinite-list-item {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 50px;
  background: var(--el-color-primary-light-9);
  margin: 10px;
  color: var(--el-color-primary);
}
.infinite-list .infinite-list-item + .list-item {
  margin-top: 10px;
}




.el-tabs--right .el-tabs__content,
.el-tabs--left .el-tabs__content {
  height: 100%;
}
/* 下面这个用来控制“我的宠物”下面一行card与上面一行对其用的*/
.empty-column {
  margin-right: 10px; /* 设置空白列宽度为20像素 */
}

.mypagepetimage{
  min-height:200px;
  width:300px;
}


</style>

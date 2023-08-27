<template>
  <div style="display: flex;align-items: center;margin-left: 5vw;">
        <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
        &nbsp;<a href="\" style="text-decoration: none;color:#538adc;">返回主页</a>
  </div>
  <div class="bigcontainer">

  <div class="common-layout">
    <el-container>
      <el-header style="height: 200px;">
        <div style="display: flex;align-items: flex-end;">
          <img src="@/photos/头像.jpg" style="width:70px;border-radius: 50%;margin-bottom: -60px;">
          <img src="@/photos/mypagepet.jpg" style="width:400px;margin-bottom: -60px;margin-left:24vw">
        </div>
       
        <el-descriptions
    class="margin-top"
    :column="3"
    :size="size"
    border
  >
    <template #extra>
      <el-button type="primary" @click="onEdit">编辑</el-button>
    </template>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <user />
          </el-icon>
         用户名
        </div>
      </template>
     {{ userStore.userInfo.User_Name }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <iphone />
          </el-icon>
          Telephone
        </div>
      </template>
      {{ userStore.userInfo.Phone_Number }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <star />
          </el-icon>
          点赞量
        </div>
      </template>
      {{ infoform.like_num }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <tickets />
          </el-icon>
          阅读量
        </div>
      </template>
      {{ infoform.read_num }}
    </el-descriptions-item>
    <el-descriptions-item>
      <template #label>
        <div class="cell-item">
          <el-icon :style="iconStyle">
            <office-building />
          </el-icon>
         地址
        </div>
      </template>
      {{ userStore.userInfo.Address }}
      <!-- 上海市嘉定区曹安公路4800号 -->
    </el-descriptions-item>
  </el-descriptions>


      </el-header>


      <el-main>
        <el-tabs v-model="activeName" class="demo-tabs" @tab-click="handleClick" type="border-card">
        <el-tab-pane label="我的宠物" name="first">
          <el-tabs tab-position="left"  class="demo-tabs">
            <el-tab-pane label="我的点赞">
              <!-- <div v-for="pet in filteredPets" :key="pet.id" class="pet-card"> -->
                  <el-card :body-style="{ padding: '0px' }" style="width: 300px;height:400px">
                      <img src="../../../public/home5.jpg" class="mypagepetimage">
                      <div style="padding: 14px;display: flex;
                        justify-content: center;
                        flex-direction: column; ">
                        <span style="font-size: 40px; color:#edb055;font-weight: bold;">花花</span>
                        <br>
                        <span style="font-size: 18px">妹妹</span>
                        <span style="font-size: 16px; color:#6b6a68">10岁</span>
                        <br>
                      </div>
                      <!-- <router-link :to="{ name: 'pet_details', params: { id: pet.id } }" style=" text-decoration: none;
                      color:#edb055 ;">
                        查看详情  —>
                      </router-link> -->
                    </el-card>
                <!-- </div> -->
            </el-tab-pane>
            <el-tab-pane label="我的收藏">
               <!-- <div v-for="pet in filteredPets" :key="pet.id" class="pet-card"> -->
                <el-card :body-style="{ padding: '0px' }" style="width: 300px;height:400px">
                      <img src="../../../public/home5.jpg" class="mypagepetimage">
                      <div style="padding: 14px;display: flex;
                        justify-content: center;
                        flex-direction: column; ">
                        <span style="font-size: 40px; color:#edb055;font-weight: bold;">花花</span>
                        <br>
                        <span style="font-size: 18px">妹妹</span>
                        <span style="font-size: 16px; color:#6b6a68">10岁</span>
                        <br>
                      </div>
                      <!-- <router-link :to="{ name: 'pet_details', params: { id: pet.id } }" style=" text-decoration: none;
                      color:#edb055 ;">
                        查看详情  —>
                      </router-link> -->
                    </el-card>
                <!-- </div> -->
            </el-tab-pane>
            <el-tab-pane label="我的评论">
              <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="$router.push('/post_details')"> -->
                  <el-card class="mypage-card" shadow="always" >
                    <template #header>
                      <div class="mypagecard-header">
                        <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center; margin-left: 5px;margin-top:-10px">花花</span>
                      </div>
                    </template>
                    <div class="mypagecardbody">
                      <div style="display: flex; align-items: center; margin-left: 5px;font-size: 15px;">
                        <img src="@/photos/头像.jpg" style="width: 30px; height: 30px; border-radius: 50%;">
                        <span style="margin-left: 5px;">Kristian:</span>
                        <span>花花头上有三种颜色欸！</span>
                      </div>            
                      <div class="mypagecardtime">2023-8-25</div>
                      <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
                    </div>
                  </el-card>
              <!-- </li> -->
              
            </el-tab-pane>
            <el-tab-pane label="我的领养">
               <!-- <div v-for="pet in filteredPets" :key="pet.id" class="pet-card"> -->
                <el-card :body-style="{ padding: '0px' }" style="width: 300px;height:400px">
                      <img src="../../../public/home5.jpg" class="mypagepetimage">
                      <div style="padding: 14px;display: flex;
                        justify-content: center;
                        flex-direction: column; ">
                        <span style="font-size: 40px; color:#edb055;font-weight: bold;">花花</span>
                        <br>
                        <span style="font-size: 18px">妹妹</span>
                        <span style="font-size: 16px; color:#6b6a68">10岁</span>
                        <br>
                      </div>
                      <!-- <router-link :to="{ name: 'pet_details', params: { id: pet.id } }" style=" text-decoration: none;
                      color:#edb055 ;">
                        查看详情  —>
                      </router-link> -->
                    </el-card>
                <!-- </div> -->
            </el-tab-pane>
            <el-tab-pane label="我的寄养">
              <el-card class="mypagefoster" shadow="always">
                <template #header>
                  <div class="mypagecard-header">
                    <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center;margin-top:-10px">花花</span>
                  </div>
                </template>
                <div class="mypagefostertext" style="margin-top:2px">寄养时长：7天</div>
                <div class="mypagefostertext">寄养起始时间：</div>
                <div class="mypagefostertext">寄养费用：</div>
              </el-card>
            </el-tab-pane>
          </el-tabs>
        </el-tab-pane>



        <el-tab-pane label="我的论坛" name="second">
          <el-tabs tab-position="left"  class="demo-tabs">
            <el-tab-pane label="我的点赞">
              <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="$router.push('/post_details')"> -->
                <el-card class="post-card" shadow="always" >
                  <div class="post-title">这是一个帖子标题</div>
                  <div class="post-info">
                    <div>发表时间：2023-8-25</div>
                    <div>阅读量：100</div>
                    <div>喜爱数量：10</div>
                    <div>评论数量：10</div>
                    <!-- <el-button class="postbutton" type="plain" text style="text-align: center;justify-content: center;">查看详情</el-button> -->
                  </div>
                </el-card>
            <!-- </li> -->
            </el-tab-pane>
            <el-tab-pane label="我的评论">
              <!-- <li v-for="post in filteredPosts" :key="post.post_id" @click="$router.push('/post_details')"> -->
                <el-card v-for="postcom in postcomment" class="mypage-card" shadow="always">
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
            </el-tab-pane>
            <el-tab-pane label="我的捐款">
              <el-card v-for="dona in donations" class="mypagedonate" shadow="always">
                <!-- <template #header>
                  <div class="mypagecard-header">
                    <span class="mypagecardtime" style="font-weight: bold;font-size: 15px;align-items: center;margin-top:-10px">花花</span>
                  </div>
                </template> -->
                <div class="mypagefostertext" style="margin-top:2px">捐款金额：{{ dona.amount }}</div>
                <div class="mypagefostertext">捐款时间：{{ dona.time }}</div>
              </el-card>
            </el-tab-pane>

          </el-tabs>
        </el-tab-pane>
        
      </el-tabs>
        
      </el-main>
    
    </el-container>
  </div>
</div>
  
  
  

</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { TabsPaneContext } from 'element-plus'
import { useUserStore } from '@/store/user'
import userinfo from '@/api/userInfo'
import { computed } from 'vue'
import { Iphone, Location, OfficeBuilding, Tickets, User, UserFilled, Star } from '@element-plus/icons-vue'
const userStore = useUserStore()
const activeName = ref('first')
const infoform = ref({
  like_num: 0,
  read_num: 0,
});
const postcomment = ref([])
const donations = ref([])
const medicallist = ref([])

const getUserInfo = async () => {
    try {
      const response = await userinfo.userInfoAPI(userStore.userInfo.User_ID);
      infoform.value.like_num= response.likes,
      infoform.value.read_num= response.reads
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
          time:comment.comment_Time
        })
      }
    } catch (error) {
      console.error('获取用户帖子评论时出错：', error);
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

onMounted(() => {
    getUserInfo();
    getUserComment();
    getUserDonation();
    getUserMedical();
});

const handleClick = (tab: TabsPaneContext, event: Event) => {
  console.log(tab, event)
}

const tabPosition = ref('left')

const load = () => {
  count1.value += 2
  count2.value += 2
  count3.value += 2
  count4.value += 2
  count5.value += 2
  count6.value += 2
  count7.value += 2
  count8.value += 2

}
const count1 = ref(0)
const count2 = ref(0)
const count3 = ref(0)
const count4 = ref(0)
const count5 = ref(0)
const count6 = ref(0)
const count7 = ref(0)
const count8 = ref(0)


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


const blockMargin = computed(() => {
  const marginMap = {
    large: '32px',
    default: '28px',
    small: '24px',
  }
  return {
    marginTop: marginMap[size.value] || marginMap.default,
  }
})



const pet_name1 = ref('乐乐')
const pet_name2 = ref('乐乐')
const pet_name3 = ref('乐乐')
const pet_name4 = ref('乐乐')
const pet_name5 = ref('乐乐')
const pet_name6 = ref('乐乐')
const pet_name7 = ref('乐乐')
const pet_name8 = ref('乐乐')
const pet_name9 = ref('乐乐')//领养
const pet_name10 = ref('乐乐')//领养
const pet_name11 = ref('乐乐')//医疗
const pet_id1 = ref('111222')//领养
const pet_id2 = ref('111222')//领养
const pet_id3 = ref('111222')//医疗
const donor_id = ref('111000')
const donation_time = ref(2023-5-1)
const donation_amount = ref(2023)
const doctor_id = ref('111001')
const vet_id = ref('111001')
const adoption_time = ref(2023-5-2)
const start_year = ref(2022)
const start_month = ref(12)
const start_day = ref(19)


</script>

<style scoped>


.bigcontainer{
  /* justify-content: center; */
  width:100%
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
  height:10px
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
}

.mypagedonate{
  width:300px;
  height:100px;
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
  height:350px;
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
  height:200px;
  width:300px;
}

</style>

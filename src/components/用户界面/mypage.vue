<template>
  <div style="display: flex;align-items: center;">
        <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
        &nbsp;<a href="\" style="text-decoration: none;color:#538adc;">返回主页</a>
  </div>
  <div class="common-layout">
    <el-container>
      <el-header style="height: 200px;">
        <el-descriptions
    class="margin-top"
    title="我的信息"
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
     {{ user_name }}
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
      {{ phone_number }}
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
      {{ like_num }}
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
      {{ read_num }}
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
      {{ address }}
      <!-- 上海市嘉定区曹安公路4800号 -->
    </el-descriptions-item>
  </el-descriptions>


      </el-header>


      <el-main>
        <el-tabs v-model="activeName" class="demo-tabs" @tab-click="handleClick">
        <el-tab-pane label="我的宠物" name="first">
          <el-row :gutter="20">
          
          <el-col :span="6">
            <el-text class="mx-1" size="large">我的点赞</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count5" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name5 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的收藏</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count6" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name6 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
          </el-col>

         <el-col :span="6">
            <el-text class="mx-1" size="large">我的评论</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count7" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name7 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的浏览</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count8" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name8 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
            <br>
          </el-col>

          <div class="empty-column">
          <!-- 需要空一列的代码 -->
          </div>

          <el-col :span="6">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的领养</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 6" :key="o" class="text item">
                <span v-if="o === 1">宠物ID:{{ pet_id1 }}</span>
                <span v-if="o === 2">宠物名称：{{ pet_name9 }}</span>
                <span v-if="o === 3">领养时间:{{ adoption_time }}</span>
                
              </div>
            </el-card>
          </el-col>
          <el-col :span="6">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的寄养</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 7" :key="o" class="text item">
                <span v-if="o === 1">宠物ID:{{ pet_id2 }}</span>
                <span v-if="o === 2">宠物名称:{{ pet_name10 }}</span>
                <span v-if="o === 3">寄养开始时间:{{start_year}}-{{start_month}}-{{start_day}}</span>
                
                
              </div>
            </el-card>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          
          
        </el-row>
        </el-tab-pane>
        <el-tab-pane label="我的论坛" name="second">
          <el-row :gutter="20">
          <el-col :span="6">
            <el-text class="mx-1" size="large">我的点赞</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count1" :key="i" class="infinite-list-item">
                <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name1 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的收藏</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count2" :key="i" class="infinite-list-item">
              <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name2 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
          </el-col>

         <el-col :span="6">
            <el-text class="mx-1" size="large">我的评论</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count3" :key="i" class="infinite-list-item">
              <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name3 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
          </el-col>

          <el-col :span="6">
            <el-text class="mx-1" size="large">我的浏览</el-text>
            <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
            <li v-for="i in count4" :key="i" class="infinite-list-item">
              <span v-if="i === 1" style="color: black;">宠物名称：{{ pet_name4 }}</span>
                <!-- <span v-if="i === 2" style="color: black;">宠物名称：杰瑞</span>
                <span v-if="i === 3" style="color: black;">宠物名称：斯派克</span> -->
            </li>
            </ul>
          </el-col>
        </el-row>
        </el-tab-pane>
        <el-tab-pane label="其他" name="third">
          <el-row :gutter="20">
          
            <el-col :span="6">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的医疗</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 7" :key="o" class="text item">
                <span v-if="o === 1">宠物ID:{{ pet_id3 }}</span>
                <span v-if="o === 2">宠物名称：{{ pet_name11 }}</span>
                <span v-if="o === 3">医生ID：{{ doctor_id }}</span>                
                <span v-if="o === 4">治疗ID:{{ vet_id }}</span>
                
              </div>
            </el-card>
          </el-col>
          <el-col :span="6">
            <el-card class="box-card">
              <template #header>
                <div class="card-header">
                  <span>我的捐款</span>
                  <!-- <el-button class="button" text>Operation button</el-button> -->
                </div>
              </template>
              <div v-for="o in 3" :key="o" class="text item">
                <span v-if="o === 1">捐款ID:{{ donor_id }}</span>
                <span v-if="o === 2">捐款金额:{{ donation_amount }}</span>
                <span v-if="o === 3">捐款时间:{{ donation_time }}</span>
              </div>
            </el-card>
          </el-col>
        </el-row>
        </el-tab-pane>
        
      </el-tabs>

       
       
         

        
      </el-main>
      
      
    

    
    </el-container>
    
    
  <div>
	      <!-- 用户头像显示 -->
		<img src="@/photos/头像.jpg" class="avatar-container">
	</div>
   
    
  
  </div>
  
  
  

</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { TabsPaneContext } from 'element-plus'
import axios from 'axios';

const activeName = ref('first')

const handleClick = (tab: TabsPaneContext, event: Event) => {
  console.log(tab, event)
}
import { computed} from 'vue'
import {
  Iphone,
  Location,
  OfficeBuilding,
  Tickets,
  User,
  UserFilled,
  Star
} from '@element-plus/icons-vue'

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



//added 2023/7/30

const user_name = ref('柴犬奴')
const phone_number = ref('13355719896')
const like_num = ref(99)
const read_num = ref(99)
const address = ref('上海市嘉定区曹安公路4800号')
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


axios.get('/api/user')
        .then(response => {
          user_name.value = response.data;
          
        })
        .catch(error => {
          console.log(error);
        });

axios.get('/api/user')
        .then(response => {
          phone_number.value = response.data;
          
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/user')
        .then(response => {
          like_num.value = response.data;
          
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/user')
        .then(response => {
          read_num.value = response.data;
          
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/user')
        .then(response => {
          address.value = response.data;
          
        })
        .catch(error => {
          console.log(error);
        });


//我的论坛
axios.get('/api/like-list')
        .then(response => {
          pet_name1.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/like-count')
      .then(response => {
        count1.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });


axios.get('/api/collect-list')
        .then(response => {
          pet_name2.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/collect-count')
      .then(response => {
        count2.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });


axios.get('/api/comment-list')
        .then(response => {
          pet_name3.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/comment-count')
      .then(response => {
        count3.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });



axios.get('/api/view-list')
        .then(response => {
          pet_name4.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/view-count')
      .then(response => {
        count4.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });


//我的宠物
axios.get('/api/like-list')
        .then(response => {
          pet_name5.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/like-count')
      .then(response => {
        count5.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });


axios.get('/api/collect-list')
        .then(response => {
          pet_name6.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/collect-count')
      .then(response => {
        count6.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });


axios.get('/api/comment-list')
        .then(response => {
          pet_name7.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/comment-count')
      .then(response => {
        count7.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });



axios.get('/api/view-list')
        .then(response => {
          pet_name8.value = response.data; // 将返回的数据保存在pet_name数组中
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/view-count')
      .then(response => {
        count8.value = response.data; // 将返回的数据保存在count属性中
      })
      .catch(error => {
        console.log(error);
      });

//收养
axios.get('/api/adopt')
        .then(response => {
          pet_id1.value = response.data; 
          
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/adopt')
        .then(response => {
          pet_name9.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/adopt')
        .then(response => {
          adoption_time.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });


//寄养
axios.get('/api/foster')
      .then(response => {
        pet_id2.value = response.data; 
      })
      .catch(error => {
        console.log(error);
      });
axios.get('/api/foster')
        .then(response => {
          pet_name10.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/foster')
        .then(response => {
          start_year.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/foster')
        .then(response => {
          start_month.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/foster')
        .then(response => {
          start_day.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });

//医疗
axios.get('/api/treatment')
      .then(response => {
        pet_id3.value = response.data; 
      })
      .catch(error => {
        console.log(error);
      });
axios.get('/api/treatment')
        .then(response => {
          pet_name11.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/treatment')
        .then(response => {
          doctor_id.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
axios.get('/api/treatment')
        .then(response => {
          vet_id.value = response.data;
        })
        .catch(error => {
          console.log(error);
        });
  

//edit 
//编辑按钮跳转到个人信息编辑页面还没有实现（ps：ysj暑期实习也要做网页和网站，现在有点忙）

</script>

<style>
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

/* .common-layout {
  background-image: url(assets/两只狗勾.jpg);
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
} */

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

.avatar-container {
	  position: absolute;
    top:20px;
	  left: 100px;
	  width: 50px;
	  height: 50px;
	  border-radius: 999px; /* 将border-radius设置为很大的值，使容器变成圆形 */
	  background-color: #fff;
	  /* 添加其他自定义样式 */
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

</style>

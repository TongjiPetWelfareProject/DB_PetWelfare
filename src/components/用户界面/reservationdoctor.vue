<template>
      <div>
      <div style="display: flex;align-items: center;">
              <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
              &nbsp;<a href="\medical" style="text-decoration: none;color:#538adc;">返回医疗界面</a>
        </div>
      </div>
      <br>
      <div class="tableitem" >
        <img src="./photos/狗狗头像.jpeg" style="height: 100px;width: 100px;margin-left: 20px;margin-right: 20px">
        <el-text class="welcome-text" size="Large">医疗预约表</el-text>
      </div>
    <el-form :model="form" label-width="120px" style="max-width: 90%;">
      <el-form-item label="是否初次">
      
      <el-radio-group v-model="form.isOld">
      <el-radio label="它未在此治疗过" value="它未在此治疗过">它未在此治疗过</el-radio>
      <el-radio  label="它已经在此治疗过" value="它已经在此治疗过">它已经在此治疗过</el-radio>
    </el-radio-group>
</el-form-item>
    <el-form-item v-if="form.isOld==='它已经在此治疗过'">
      <el-select v-model="form.petID" class="m-2" placeholder="请选择宠物" size="small" >
        <el-option
          v-for="pet in petOptions"
          :key="pet.id"
          :label="pet.name"
          :value="pet.id"
        />
      </el-select>
    </el-form-item>
    <el-form-item v-if="form.isOld==='它未在此治疗过'">
      <el-radio-group v-model="form.pet_kind" size="small" >
        <el-radio label="狗" value="dog"/>
        <el-radio label="猫" value="cat" />
      </el-radio-group>
    </el-form-item>
      
 
      <el-form-item label="宠物姓名" v-if="form.isOld==='它未在此治疗过'">
        <el-input v-model="form.name" placeholder="请输入宠物姓名"/>
      </el-form-item>
      <el-form-item label="问题种类">
        <el-select v-model="form.kind" placeholder="请选择问题种类">
          <el-option label="皮肤问题" value="皮肤问题" />
          <el-option label="胃部问题" value="胃部问题" />
          <el-option label="耳部感染" value="耳部感染" />
          <el-option label="眼部疾病" value="眼部疾病" />
          <el-option label="疼痛" value="疼痛" />
          <el-option label="肿块" value="肿块" />
          <el-option label="泌尿道感染" value="泌尿道感染" />
          <el-option label="过敏反应" value="过敏反应" />
          <el-option label="十字韧带撕裂和手术" value="十字韧带撕裂和手术" />
          <el-option label="癌症" value="癌症" />
          <el-option label="其他" value="其他" />
        </el-select>
      </el-form-item>
      <el-form-item label="预约医生">
        <el-select v-model="form.selectedDoctorID" placeholder="请选择医生">
          <el-option
            v-for="doctor in doctors"
            :key="doctor.id"
            :label="doctor.name"
            :value="doctor.id"
          ></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="预约时间">
        <el-col :span="11">
          <el-date-picker
            v-model="form.date1"
            type="date"
            placeholder="请选择时间"
            style="width: 100%"
          />
        </el-col>
      </el-form-item>
      <div class="notice">
        <span class="red-asterisk">*</span> 医疗服务请选择从今天开始的一周内时间预约
      </div>
      <!-- <el-form-item label="Instant delivery">
        <el-switch v-model="form.delivery" />
      </el-form-item> -->
      <!-- <el-form-item label="预约项目">
        <el-checkbox-group v-model="form.type">
          <el-checkbox label="检查" name="type" />
          <el-checkbox label="治疗" name="type" />
          <el-checkbox label="手术" name="type" />
        </el-checkbox-group>
      </el-form-item>
      <el-form-item label="问诊情况">
        <el-radio-group v-model="form.resource">
          <el-radio label="首次" />
          <el-radio label="复诊" />
        </el-radio-group>
      </el-form-item> -->
      <el-form-item label="病情描述" >
        <el-input v-model="form.desc" :autosize="{ minRows: 10, maxRows: 20 }" type="textarea" placeholder="请描述宠物病情"/>
      </el-form-item>
      <el-form-item>
        <br><br>
        <el-button type="primary" @click="onSubmit">提交</el-button>
        <!-- <el-button>取消</el-button> -->
      </el-form-item>
    </el-form>
  </template>
  
<script>
import { ref,defineComponent, reactive,onMounted } from 'vue';
import medical_donate from '@/api/medical_donate';
import { ElMessage } from 'element-plus'
import { useUserStore } from '@/store/user'; // 导入用户信息管理模块
import { useRouter } from 'vue-router'



const router = useRouter()
export default defineComponent({
  setup() {

    const userStore = useUserStore();
    const doctors = ref([]);
    const petOptions = ref([]); 
    const petMap1 = ref(new Map());
    const petMap2 = ref(new Map());
    const router = useRouter();

    const form = reactive({
      name: '',
      kind: '',
      date1: '',
      desc: '',
      selectedDoctorID:'',
      petID:'',
      isOld:'它未在此治疗过',
      pet_kind:'dog'
    });

    const fetchPetInfo = async () => {
      try {
        const response = await medical_donate.getPetInfoAPI(); // 根据你的实际情况修改 API 调用  
        petOptions.value = response.data; // 使用宠物名字数组更新 petOptions
        // 建立宠物ID与宠物名字的映射关系
        petMap1.value = new Map(response.data.map(pet => [pet.id, pet.name]));
        petMap2.value = new Map(response.data.map(pet => [pet.id, pet.kind]));
      } catch (error) {
        console.error('获取宠物信息出错：', error);
      }
    };

    const onSubmit = async () => {
      try {
        if(form.isOld==='它已经在此治疗过'){
          form.name = petMap1.value.get(form.petID); // 根据映射关系获取宠物名字
          form.pet_kind=petMap2.value.get(form.petID);
        }

        const dateObject = new Date(form.date1);
        const currentDate=new Date();

        const oneWeekLater = new Date(currentDate);
        oneWeekLater.setDate(currentDate.getDate() + 7);

        // console.log(dateObject); 
        // const month = dateObject.getMonth(); // 获取月份，0 表示一月，1 表示二月，以此类推
        // const day = dateObject.getDate(); // 获取日期
        // const year = dateObject.getFullYear(); // 获取年份

        // console.log(`月份: ${month}, 日期: ${day}, 年份: ${year}`);

        // console.log(form.name)
        // console.log(form.kind)
        // console.log(form.date1)
        // console.log(form.desc)
        // console.log(form.selectedDoctorID)

        if (!form.name || !form.kind || !form.date1 || !form.desc || !form.selectedDoctorID) {
            ElMessage.warning({
              message: '预约失败,请填写完整信息' ,
              duration: 3000 // 持续显示时间（毫秒）
            });
          return; // 阻止提交
         }
		 if (dateObject.getDay() == 0||dateObject.getDay() == 6) {
		   ElMessage.warning('预约时间必须在工作日内');
		   return; // 不继续执行
		 }
      
        if (dateObject > oneWeekLater) {
          ElMessage.warning('预约时间必须在两周内');
          return; // 不继续执行
        }
        if (dateObject < currentDate) {
          ElMessage.warning('预约时间在当前时间前，请重新选择');
          return; // 不继续执行
        }


        // 调用 submitAppointmentAPI 函数并传入表单数据
        const userId = userStore.userInfo.User_ID;

        // 在表单数据中添加 userId
        form.userId = userId;
        if(form.isOld==='它已经在此治疗过'){
          form.name = petMap1.value.get(form.petID); // 根据映射关系获取宠物名字
          form.pet_kind=petMap2.value.get(form.petID);
        }
        const response = await medical_donate.submitAppointmentAPI(form);
        console.log('提交成功：', response);
        // 显示成功提示
        ElMessage.success({
          message: '预约医疗成功',
          duration: 3000 // 持续显示时间（毫秒）
        });
        // 停顿2秒后跳转到 '/forum'
        setTimeout(() => {
          router.push('/medical');
        }, 2000);

      } catch (error) {
        console.error('提交数据时出错：', error);
        // 显示失败提示
        ElMessage.error({
          message: '预约失败，错误信息：' + error.message,
          duration: 3000 // 持续显示时间（毫秒）
      });
      }
    };

    onMounted(async () => {
      doctors.value = await medical_donate.getDoctorsAPI();
      fetchPetInfo();
    });

    return {
      form,
      onSubmit,
      doctors,
      petOptions,
      petMap1,
      petMap2
    };
  },
});

  </script>

<style>
.notice {
  font-size: small; /* 使用小字形式的字体大小 */
  margin-top: 10px; /* 可以根据需要调整顶部边距 */
  margin-bottom: 15px;
  color: #666; /* 可以根据需要调整文字颜色 */
  margin-left: 105px;
}

.red-asterisk {
  color: red; /* 红色的星号颜色 */
  margin-right: 5px; /* 可以根据需要调整星号与文字之间的间距 */
}
</style>

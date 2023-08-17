<template>
     <el-text size="large" tag="b" style="margin-left:50px">预约问诊</el-text>
     <br><br>
    <el-form :model="form" label-width="120px" style="max-width: 90%;">
      <el-form-item label="宠物姓名">
        <el-input v-model="form.name"/>
      </el-form-item>
      <el-form-item label="种类">
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
      <el-form-item label="病情描述">
        <el-input v-model="form.desc" type="textarea" />
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

export default defineComponent({
  setup() {

    const doctors = ref([]);
   

    const form = reactive({
      name: '',
      kind: '',
      date1: '',
      desc: '',
      selectedDoctorID:'',
    });

    const onSubmit = async () => {
      try {
        // 调用 submitAppointmentAPI 函数并传入表单数据
        const response = await medical_donate.submitAppointmentAPI(form);
        console.log('提交成功：', response);
      } catch (error) {
        console.error('提交数据时出错：', error);
      }
    };

    onMounted(async () => {
      doctors.value = await medical_donate.getDoctorsAPI();
    });

    return {
      form,
      onSubmit,
      doctors,
    };
  },
});
  </script>
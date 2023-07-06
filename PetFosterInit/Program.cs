// See https://aka.ms/new-console-template for more information
using Database_CourseDesign;

//以下是程序入口
Console.WriteLine("大家好，这是数据库的初始化项目，请先进行config.json中systemPwd，PicturePath与TablespacePath的配置！！！");

//获取项目脚本所在路径

ManageSystemInit manageSystemInit = new ManageSystemInit();
manageSystemInit.PreInit();

/**
//数据库连接测试
Test test = new Test(programPath);
test.ConnectTest();
test.PrintTableTest();
/**/
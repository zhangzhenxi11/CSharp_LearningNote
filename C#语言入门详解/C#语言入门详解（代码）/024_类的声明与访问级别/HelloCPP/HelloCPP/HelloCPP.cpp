// HelloCPP.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
#include "Student.h"

int main()
{
	Student* stu = new Student();//动态分配内存创建类对象
	stu->SayHello();    //C++成员访问操作符->
	return 0;
}

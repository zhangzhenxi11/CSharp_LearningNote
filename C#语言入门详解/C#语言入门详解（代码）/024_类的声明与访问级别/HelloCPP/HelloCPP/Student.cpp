#include "Student.h"
#include "iostream"

Student::Student() {//C++类的定义，cpp文件

}
Student::~Student() {

}
void Student::SayHello() {//删除方法SayHello，如果有调用，编译时连接器会报错
	std::cout << "Hello!" << std::endl;
}
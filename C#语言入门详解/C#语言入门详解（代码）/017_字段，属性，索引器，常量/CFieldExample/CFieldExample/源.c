#include<stdio.h>

struct Student
{
	int ID;
	char* Name;
};

void main()
{
	struct Student stu;
	stu.ID = 1;
	stu.Name = "Mr.Okay";
	printf("Student #%d is %s.", stu.ID, stu.Name);
}
package com.hello;

import java.util.Scanner;

public class Main {


    public static void main(String[] args) {
	// write your code here
        CompareNumber();

        add();

        con3();
        con2();
        con();

        Test t=new Test();
        var r= t.add(1,10);
        System.out.println(r);

        /*多行
        注释*/
        System.out.println("    *");
        System.out.println("   ***");
        System.out.println("  *****");
        System.out.println(" *******");
        System.out.println("*********");

    }

    public static void CompareNumber() {
        int number1, number2; // 定义变量，保存输入的两个数
        System.out.print("请输入第一个整数(number1)：");
        Scanner input = new Scanner(System.in);
        number1 = input.nextInt(); // 输入第一个数
        System.out.print("请输入第二个整数(number2)：");
        input = new Scanner(System.in);
        number2 = input.nextInt(); // 输入第二个数
        System.out.printf("number1=%d,number2=%d\n", number1, number2); // 输出这两个数
        // 判断用户输入的两个数是否相等
        if (number1 == number2) {
            System.out.println("number1 和 number2 相等。");
        }
        // 判断用户输入的两个数据是否相等
        if (number1 != number2) {
            System.out.println("number1 和 number2 不相等。");
            // 判断用户输入的数1是否大于数2
            if (number1 > number2) {
                System.out.println("number1 大于 number2。");
            }
            // 判断用户输入的数1是否小于数2
            if (number1 < number2) {
                System.out.println("number1 小于 number2。");
            }
        }
    }

    public static void con() {
        byte a = 20; // 声明一个byte类型的变量并赋予初始值为20
        short b = 10; // 声明一个short类型的变量并赋予初始值为10
        int c = 30; // 声明一个int类型的变量并赋予初始值为30
        long d = 40; // 声明一个long类型的变量并赋予初始值为40
        long sum = a + b + c + d;
        System.out.println("20+10+30+40=" + sum);
    }

    public static void con2() {
        String s="111";
        byte a = 20; // 声明一个byte类型的变量并赋予初始值为20
        short b = 10; // 声明一个short类型的变量并赋予初始值为10
        int c = 30; // 声明一个int类型的变量并赋予初始值为30
        long d = 40; // 声明一个long类型的变量并赋予初始值为40
        String sum = a + b + c + d+s;
        System.out.println("20+10+30+40=" + sum);
    }

    public static void con3() {
        String s="111";
        byte a = 20; // 声明一个byte类型的变量并赋予初始值为20
        short b = 10; // 声明一个short类型的变量并赋予初始值为10
        int c = 30; // 声明一个int类型的变量并赋予初始值为30
        long d = 40; // 声明一个long类型的变量并赋予初始值为40
        String sum =s+ a + b + c + d;
        System.out.println("20+10+30+40=" + sum);
    }

    public static void add() {
        char a = 'A';    // 向 char 类型的 a 变量赋值为 A，所对应的 ASCII 值为 65
        char b = 'B';    // 向 char 类型的 b 变量赋值为 B，所对应的 ASCII 值为 66
        System.out.println("A 的 ASCII 值与 B 的 ASCII 值相加结果为："+(a+b));
    }
}

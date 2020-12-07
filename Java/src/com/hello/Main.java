package com.hello;

import java.util.Scanner;

public class Main {


    public static void main(String[] args) {
        IntToString();

        StringToInt();

        StringTest();

        YanghuiTriangle();

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

    private static void IntToString() {
        int num = 10;
        // 第一种方法：String.valueOf(i);
        num = 10;
        String str = String.valueOf(num);
        System.out.println("str:" + str);
        // 第二种方法：Integer.toString(i);
        num = 10;
        String str2 = Integer.toString(num);
        System.out.println("str2:" + str2);
        // 第三种方法："" + i;
        String str3 = num + "";
        System.out.println("str3:" + str3);
    }

    private static void StringToInt() {
        String str = "123";
        int n = 0;
        // 第一种转换方法：Integer.parseInt(str)
        n = Integer.parseInt(str);
        System.out.println("Integer.parseInt(str) : " + n);
        // 第二种转换方法：Integer.valueOf(str).intValue()
        n = 0;
        var m=Integer.valueOf(str);
        n = Integer.valueOf(str).intValue();
        System.out.println("Integer.parseInt(str) : " + n);
    }

    private static void YanghuiTriangle() {
        Scanner scan = new Scanner(System.in);
        System.out.print("打印杨辉三角形的行数：");
        int row = scan.nextInt();
        calculate(row);
    }

    private static void StringTest() {
        char a[] = {'H','e','l','l','0'};
        String sChar = new String(a);
        a[1] = 's';
        System.out.println(sChar);

        var s="11123423f";
        System.out.println(s);

        String str = "我是一只小小鸟"; // 结果：我是一只小小鸟
        String word;
        word = "I am a bird"; // 结果：I am a bird
        word = "<h1>to fly</h1>"; // 结果：<h1>to fly</h1>
        word = "Let\'s say that it\'s true"; // 结果：Let's say that it's true
        System.out.println(word);
        word = "北京\\上海\\广州"; // 结果：北京\上海\广州
        System.out.println(word);

        var a2= String.valueOf(a,0,a.length-1);

        
    }

    public static void calculate(int row) {
        for (int i = 1; i <= row; i++) {
            for (int j = 1; j <= row - i; j++) {
                System.out.print(" ");
            }
            for (int j = 1; j <= i; j++) { // 打印空格后面的字符, 从第1 列开始往后打印
                System.out.print(num(i, j) + " ");
            }
            System.out.println();
        }
    }

    public static int num(int x, int y) {
        if (y == 1 || y == x) {
            return 1;
        }
        int c = num(x - 1, y - 1) + num(x - 1, y);
        return c;
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

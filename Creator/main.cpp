#include <stdio.h>
#include <stdlib.h>

const int CONTAIN_MAX = 15;
const int CONTAIN_INT_RADIX = 15;
const int BLANK_NUM_OF_INDENTATION = 2;
const char* file_path = "C://homework.xml";

enum DifficultyValue
{
	HARD,
	MEDIUM,
	EASY
};

struct Difficulty
{
	DifficultyValue difficulty;
	static const char* TAG;
};
// Never mind this statement.This is c++ syntax to init static const pointer.
const char* Difficulty::TAG = "difficulty";

struct Question
{
	Difficulty difficulty;
	int number;// qustion No
	static const char* TAG;
};
const char* Question::TAG = "question";

struct Chapter
{
	int question_num = 0;// amount of questions in the chapter
	Question questions[CONTAIN_MAX];
	static const char* TAG;
};
const char* Chapter::TAG = "chapter";

struct Subject
{
	int charpter_num = 0;// amount of chapters in the subject
	Chapter chapters[CONTAIN_MAX];
	const char *subject_name;
	static const char* TAG;
};
const char* Subject::TAG = "subject";

struct Homework
{
	int subject_num = 0;// amount of subjects in the homework
	Subject subjects[CONTAIN_MAX];
	static const char* TAG;
};
const char* Homework::TAG = "homework";

/* Print a certain string.*/
void print_string(FILE *f, const char const *s)
{
	if (s != NULL && f != NULL)
	{
		fprintf(f, s);
	}
}

/* Print blanks.*/
void print_indentation(FILE *f, int num)
{
	for (int i = 0; i < num; i++)
		print_string(f, " ");
}

/* 
	Print tags with atrributes.
	Example: <hello name="123">
	Param:
	indentation blank num of indentation.
	attrc attribute count.
	attrn attribute name.
	attrv attribute value.
*/
void print_tag(FILE *f, const char* tag, int indentation, int attrc, const char *attrn[], const char *attrv[])
{
	print_indentation(f, indentation);
	print_string(f, "<");
	print_string(f, tag);

	for (int i = 0; i < attrc;i ++)
	{
		if (attrn[i] != NULL && attrv[i] != NULL)
		{
			print_string(f, " ");
			print_string(f, attrn[i]);
			print_string(f, "=\"");
			print_string(f, attrv[i]);
			print_string(f, "\"");
		}
	}

	print_string(f, ">\n");	
}

/* 
	Print back tags.
	Example: </hello>
	Param:
	indentation blank num of indentation.
*/
void print_back_tag(FILE *f, const char* tag, int indentation)
{
	print_indentation(f, indentation);
	print_string(f, "</");
	print_string(f, tag);
	print_string(f, ">\n");
}

/*
	Print difficulty tag.
	Param:
	indentation blank num of indentation.
*/
void print_difficulty(FILE *f, Difficulty difficulty, int indentation)
{
	print_tag(f, difficulty.TAG, indentation, 0, NULL, NULL);

	print_indentation(f, indentation + BLANK_NUM_OF_INDENTATION);
	char *buff = (char*)malloc(sizeof(char) * CONTAIN_INT_RADIX);
	itoa(difficulty.difficulty, buff, 10);// transform int into char, why do I do this£¿Or why not use fprintf()?
	print_string(f, buff);
	print_string(f, "\n");

	print_back_tag(f, difficulty.TAG, indentation);

	free(buff);
}

/*
	Print question tag.
	Param:
	indentation blank num of indentation.
*/
void print_qustion(FILE *f, Question question, int indentation)
{
	char *buff = (char*) malloc(sizeof(char) * CONTAIN_INT_RADIX);
	itoa(question.number, buff, 10);// transform int into char, why do I do this£¿Or why not use fprintf()?
	const char *attrn[] = { "No" };
	const char *attrv[] = { buff };

	print_tag(f, question.TAG, indentation, 1, attrn, attrv);
	print_difficulty(f, question.difficulty, indentation + BLANK_NUM_OF_INDENTATION);
	print_back_tag(f, question.TAG, indentation);

	free(buff);
}

/*
	Print chapter tag.
	Param:
	indentation blank num of indentation.
*/
void print_chapter(FILE *f, Chapter chapter, int indentation)
{
	print_tag(f, chapter.TAG, indentation, 0, NULL, NULL);

	for (int i = 0; i < chapter.question_num; i++)
	{
		print_qustion(f, chapter.questions[i], indentation + BLANK_NUM_OF_INDENTATION);
	}

	print_back_tag(f, chapter.TAG, indentation);
}

/*
	Prinf subject tag.
	Param:
	indentation blank num of indentation.
*/
void print_subject(FILE *f, Subject subject, int indentation) 
{
	const char *attrn[] = { "Name" };
	const char *attrv[] = { subject.subject_name };

	print_tag(f, subject.TAG, indentation, 1, attrn, attrv);

	for (int i = 0; i < subject.charpter_num; i++)
	{
		print_chapter(f, subject.chapters[i], indentation + BLANK_NUM_OF_INDENTATION);
	}

	print_back_tag(f, subject.TAG, indentation);
}

/*
	Print homework tag.
	Param:
	indentation blank num of indentation.
*/
void print_homework(FILE *f, Homework homework, int indentation)
{
	print_tag(f, homework.TAG, indentation, 0, NULL, NULL);

	for (int i = 0; i < homework.subject_num; i++)
	{
		print_subject(f, homework.subjects[i], indentation + BLANK_NUM_OF_INDENTATION);
	}

	print_back_tag(f, homework.TAG, indentation);
}

/*
	You can replace it with your way to init homework.
*/
Homework init_homework()
{
	Homework homework;
	static const char *subject_name[] = { "Math", "Chinese", "Program"};

	// subjects
	for (int i = 0; i < 2; i ++)
	{
		homework.subject_num ++;
		//chapters
		for (int j = 0; j < 2; j ++)
		{
			homework.subjects[i].charpter_num ++;
			homework.subjects[i].subject_name = subject_name[i];
			// questions
			for (int k = 0; k < 2; k ++)
			{
				homework.subjects[i].chapters[j].questions[k].difficulty.difficulty = HARD;
				homework.subjects[i].chapters[j].questions[k].number = k;
				homework.subjects[i].chapters[j].question_num ++;
			}
		}
	}
	return homework;
}

void main(int argc, char* argv[])
{
	FILE *file;
	file = fopen(file_path, "w");
	if (file == NULL)
	{
		file = fopen(file_path, "a+");
	}

	print_homework(file, init_homework(), 0);
}
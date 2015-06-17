#include <stdio.h>
#include <stdlib.h>

const int CONTAIN_MAX = 5;
const int CONTAIN_INT_RADIX = 15;
const int BLANK_NUM_OF_INDENTATION = 2;

enum DifficultyValue
{
	HARD,
	MEDIUM,
	EASY
};

struct Difficulty
{
	DifficultyValue difficulty;
	const char* TAG = "difficulty";
};

struct Question
{
	Difficulty difficulty;
	int number;// qustion No
	const char* TAG = "question";
};

struct Chapter
{
	int question_num;// amount of questions in the chapter
	Question questions[CONTAIN_MAX];
	const char* TAG = "chapter";
};

struct Subject
{
	int charpter_num;// amount of chapters in the subject
	Chapter charpters[CONTAIN_MAX];
	const char* TAG = "subject";
};

struct Homework
{
	int subject_num;// amount of subjects in the homework
	Subject subjects[CONTAIN_MAX];
	const char* TAG = "homework";
};

void print_string(FILE *f, const char const *s)
{
	if (s != NULL && f != NULL)
	{
		fprintf(f, s);
	}
}

void print_indentation(FILE *f, int num)
{
	for (int i = 0; i < num; i++)
		print_string(f, " ");
}

void print_tag(FILE *f, const char* tag, int indentation, int attrc, char *attrn[], char *attrv[])
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

void print_back_tag(FILE *f, const char* tag, int indentation)
{
	print_indentation(f, indentation);
	print_string(f, "</");
	print_string(f, tag);
	print_string(f, ">\n");
}

void print_difficulty(FILE *f, Difficulty difficulty, int indentation)
{
	print_tag(f, difficulty.TAG, indentation, 0, NULL, NULL);
	char *buff = (char*)malloc(sizeof(char) * CONTAIN_INT_RADIX);
	itoa(difficulty.difficulty, buff, 10);// transform int into char, why do I do this£¿Or why not use fprintf()?
	print_string(f, buff);
	print_back_tag(f, difficulty.TAG, indentation);

	free(buff);
}

void print_qustion(FILE *f, Question question, int indentation)
{
	char *buff = (char*) malloc(sizeof(char) * CONTAIN_INT_RADIX);
	itoa(question.number, buff, 10);// transform int into char, why do I do this£¿Or why not use fprintf()?
	char *attrn[] = { "No" };
	char *attrv[] = { buff };

	print_tag(f, question.TAG, indentation, 1, attrn, attrv);
	print_difficulty(f, question.difficulty, indentation + BLANK_NUM_OF_INDENTATION);
	print_back_tag(f, question.TAG, indentation);
	free(buff);
}

void print_chapter(FILE *f, Chapter chapter, int indentation)
{
	print_tag(f, chapter.TAG, indentation, 0, NULL, NULL);
	for (int i = 0; i < chapter.question_num; i++)
	{
		print_qustion(f, chapter.questions[i], indentation + BLANK_NUM_OF_INDENTATION);
	}
	print_back_tag(f, chapter.TAG, indentation);
}

void print_subject(FILE *f, Subject subject, int indentation) 
{
	print_tag(f, subject.TAG, indentation, 0, NULL, NULL);
	for (int i = 0; i < subject.charpter_num; i++)
	{
		print_chapter(f, subject.charpters[i], indentation + BLANK_NUM_OF_INDENTATION);
	}
	print_back_tag(f, subject.TAG, indentation);
}

void print_homework(FILE *f, Homework homework, int indentation)
{
	print_tag(f, homework.TAG, indentation, 0, NULL, NULL);
	for (int i = 0; i < homework.subject_num; i++)
	{
		print_subject(f, homework.subjects[i], indentation + BLANK_NUM_OF_INDENTATION);
	}
	print_back_tag(f, homework.TAG, indentation);
}



Homework init_homework()
{
	Homework homework;
	homework.subject_num = 1;
	homework.subjects[0].charpter_num = 1;
	homework.subjects[0].charpters[0].question_num = 1;
	homework.subjects[0].charpters[0].questions[0].difficulty.difficulty = HARD;
	homework.subjects[0].charpters[0].questions[0].number = 1;

	return homework;
}

void main(int argc, char* argv[])
{
	FILE *file;
	file = fopen("C://homework.xml", "w");

	print_homework(file, init_homework(), 0);
}
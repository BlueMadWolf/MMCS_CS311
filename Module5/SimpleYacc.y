%{
// Ёти объ€влени€ добавл€ютс€ в класс GPPGParser, представл€ющий собой парсер, генерируемый системой gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
%}

%output = SimpleYacc.cs

%namespace SimpleParser

%token BEGIN END CYCLE INUM RNUM ID ASSIGN SEMICOLON WHILE DO REPEAT UNTIL FOR TO OPBRACKET CLBRACKET WRITE IF THEN ELSE COMMA VAR PLUS MINUS DIV MULT 

%%

progr   : block
		;

stlist	: statement 
		| stlist SEMICOLON statement 
		;

statement: assign
		| block  
		| cycle
		| while  
		| repeat
		| for
		| write
		| if
		| var
		;

ident 	: ID 
		;

expr	: T
		| expr PLUS T
		| expr MINUS T
		;

T		: F
		| T MULT F
		| T DIV F
		;

F		: ident
		| INUM 
		| OPBRACKET expr CLBRACKET
		;


assign 	: ident ASSIGN expr 
		;

block	: BEGIN stlist END 
		;

cycle	: CYCLE expr statement 
		;

while   : WHILE expr DO statement
		;

repeat  : REPEAT stlist UNTIL expr 
		;

for     : FOR ID ASSIGN expr TO expr DO statement
		;

write   : WRITE OPBRACKET expr CLBRACKET 
		;


if		: IF expr THEN statement
		| if ELSE statement
		;

idlist  : ident
        | idlist COMMA ident
		;
		
var     : VAR idlist
        ;


%%

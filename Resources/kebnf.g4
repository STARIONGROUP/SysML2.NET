// Grammar

grammar kebnf;

specification : (NL)* rule_definition+ EOF ;

rule_definition
    : name=UPPER_ID (params=parameter_list)? (COLON target_ast=UPPER_ID)? ASSIGN rule_body=alternatives SEMICOLON? NL+
    ;

parameter_list
    : LPAREN param_name=ID COLON param_type=ID RPAREN
    ;
    
alternatives
    : alternative (PIPE alternative)*
    ;

alternative
    : element*
    ;

element
    : assignment
    | non_parsing_assignment
    | non_parsing_empty  
    | cross_reference
    | group
    | terminal
    | non_terminal
    | value_literal
    ;

assignment
    : property=dotted_id op=(ASSIGN | ADD_ASSIGN | BOOL_ASSIGN) (prefix=TILDE)?content=element_core (suffix=suffix_op)?
    ;

non_parsing_assignment
    : LBRACE property=dotted_id op=(ASSIGN | ADD_ASSIGN) val=value_literal RBRACE
    ;

non_parsing_empty
    : LBRACE RBRACE
    ;

cross_reference
    : TILDE? LBRACK ref=ID RBRACK
    ;

group
    : LPAREN alternatives RPAREN (suffix=suffix_op)?
    ;

terminal
    : val=SINGLE_QUOTED_STRING (suffix=suffix_op)?
    ;

non_terminal
    : name=UPPER_ID (suffix=suffix_op)?
    ;

element_core
    : cross_reference
    | group
    | terminal
    | non_terminal
    | value_literal
    ;

dotted_id
    : ID (DOT ID)*
    ;

suffix_op : '*' | '+' | '?' ;

value_literal : ID | 'true' | 'false' | 'this' | INT | STRING | '[QualifiedName]' | SINGLE_QUOTED_STRING;

// Lexer
ASSIGN : '::=' | '=' ;
ADD_ASSIGN : '+=' ;
BOOL_ASSIGN : '?=' ;
PIPE : '|' ;
COLON : ':' ;
SEMICOLON : ';' ;
COMMA : ',' ;
LPAREN : '(' ;
RPAREN : ')' ;
LBRACK : '[' ;
RBRACK : ']' ;
LBRACE : '{' ;
RBRACE : '}' ;
DOT  : '.' ;
TILDE : '~' ;

UPPER_ID : [A-Z] [a-zA-Z0-9_]* ;
ID : [a-zA-Z_][a-zA-Z0-9_]* ;
SINGLE_QUOTED_STRING : '\'' (~['\\] | '\\' .)* '\'' ;
INT : [0-9]+ ;
STRING : '\'' ( ~['\\] | '\\' . )* '\'' ;

COMMENT : '//' ~[\r\n]* -> skip ;
WS : [ \t]+ -> skip ;
CONTINUATION : '\r'? '\n' [ \t]+ -> skip ;
NL : '\r'? '\n' ;
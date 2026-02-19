// Grammar

grammar kebnf;

specification : (NL)* rule_definition+ EOF ;

rule_definition
    : name=ID (params=parameter_list)? (COLON target_ast=ID)? ASSIGN rule_body=alternatives SEMICOLON? NL+
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
    : val=value_literal (suffix=suffix_op)?
    ;

non_terminal
    : name=ID (suffix=suffix_op)?
    ;

element_core
    : cross_reference
    | group
    | terminal
    | non_terminal
    ;

dotted_id
    : ID (DOT ID)*
    ;

suffix_op : '*' | '+' | '?' ;

value_literal : ID | 'true' | 'false' | 'this' | INT | STRING | '[QualifiedName]';

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

ID : [a-zA-Z_][a-zA-Z0-9_]* ;
INT : [0-9]+ ;
STRING : '\'' ( ~['\\] | '\\' . )* '\'' ;

COMMENT : '//' ~[\r\n]* -> skip ;
WS : [ \t]+ -> skip ;
CONTINUATION : '\r'? '\n' [ \t]+ -> skip ;
NL : '\r'? '\n' ;
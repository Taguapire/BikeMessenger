
-- SELECT genhtml('public', 'tablenamehere', 'r', ARRAY['col1', 'col2', 'col3']);

CREATE OR REPLACE FUNCTION genhtml (text, text, text, text[])
   RETURNS text AS $body$
DECLARE
   schemaname ALIAS FOR $1; 
   tablename ALIAS FOR $2; 
   tabletype ALIAS FOR $3; 
   columnnames ALIAS FOR $4; 
   result TEXT := ''; 
   searchsql TEXT := '';
   varmatch TEXT := '';
   col RECORD; 
   html_doctype TEXT := '<!DOCTYPE html>' || E'\n'; 
   html_meta TEXT := '<meta charset="uft-8">' || E'\n\t' || '<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit="no">' || E'\n'; 
   html_link TEXT := '<link rel="stylesheet" href="boostrapCSSLinkHere.css">' || E'\n';
   html_bscript TEXT := '<script src="bootstraptScriptHere.js"> </script>' || E'\n';
   html_jscript TEXT := '<script src="jQueryScriptHere.js"> </script>' || E'\n'; 
   html_head TEXT := '<html>' || E'\n' || '<head>' || E'\n\t' || html_meta || E'\t' || hmtml_link || E'\t' || html_jscript ||  E'\t' || html_bscript || '</head>' || E'\n'; 
   html_body TEXT := '<body>';
   header TEXT; 
BEGIN
   header := E'\t'|| '<tr>' || E'\n'; 
   searchsql := $QUERY$SELECT ''$QUERY$; 

   FOR col IN select attname
      FROM pg_attribute AS a
      JOIN pg_class AS c ON a.attrelid = c.oid
      JOIN pg_namespace AS n ON n.oid = c.relnamespace
      WHERE c.relname = tablename
      AND n.nspname = schemaname
      AND c.relkind = tabletype
      AND attnum > 0 
      AND attname = ANY(columnnames)
   LOOP
      header := header || E'\t\t' || '<th>' || col || '</th>' || E'\n';
      searchsql := searchsql || $QUERY$ || E'\n\n\t' || '<td>' || $QUERY$ || 'coalesce(' || col || ', ''N/A'')' || $QUERY$ || '<td>' $QUERY$;
   END LOOP; 

   header := header || E'\t' || '<tr>' || E'\n'; 
   searchsql := searchsql || ' FROM ' || schemaname || '.' || tablename;
   result := html_doctype || html_head || html_body || E'\n\t' || '<table class="table table-striped table-hover">' || E'\n'; 
   result := result || header; 

   FOR varmatch IN EXECUTE (searchsql) LOOP
      IF result > '' THEN
         result := result || E'\t' || '<tr>' || varmatch || E'\n\t' || </tr> || E'\n';
      END IF;
   END LOOP;
   result := result || E'\t' || </table> || E'\n' || '</body> || E'\n' || '</html>'; 

   RETURN result; 

END; 
$body$
   LANGUAGE 'plpgsql' VOLATILE;
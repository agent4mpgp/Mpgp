FROM nshou/elasticsearch-kibana AS mpgpelastickibana

USER root 
RUN apk add curl

USER elasticsearch

COPY ./docker/mpgpelastickibana/index.json /home/elasticsearch/

CMD sh kibana/bin/kibana --host 0.0.0.0 -Q \
    & sh elasticsearch/bin/elasticsearch -E http.host=0.0.0.0 --quiet \
    & sleep 60 \
    && curl -XPOST http://localhost:5601/api/saved_objects/index-pattern/13fd5de0-d09f-11e9-ac5e-6b76646c8bd0 -H 'Content-Type: application/json' -H 'kbn-xsrf: true' -d @index.json \
    && sh

#curl -XGET http://localhost:5601/api/saved_objects/index-pattern/13fd5de0-d09f-11e9-ac5e-6b76646c8bd0 >> index.json
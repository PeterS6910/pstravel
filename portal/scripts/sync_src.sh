set -xe
rsync -avz --exclude=.git/ --exclude=node_modules/ --exclude=.env --exclude=build/ \
/c/w4/pstravel/portal/ root@usrv:/root/psp/portal/
